from rest_framework import status
from rest_framework.decorators import api_view
from rest_framework.response import Response
from rest_framework.views import APIView
from django.shortcuts import get_object_or_404
from .models import Account, Wine, Comment, Rating
from .serializers import (
    AccountSerializer, WineSerializer, CommentSerializer, RatingSerializer,
    CreateRatingSerializer, UpdateRatingSerializer, UpdateCommentSerializer,
    LoginAccountSerializer
)


class AccountListCreateView(APIView):
    """Handle GET (list all) and POST (create) for accounts"""
    
    def get(self, request):
        """Get all accounts"""
        accounts = Account.objects.all()
        serializer = AccountSerializer(accounts, many=True)
        return Response(serializer.data)
    
    def post(self, request):
        """Create account"""
        serializer = AccountSerializer(data=request.data)
        if serializer.is_valid():
            if not request.data.get('password'):
                return Response(
                    {"error": "Password must not be null"}, 
                    status=status.HTTP_400_BAD_REQUEST
                )
            account = serializer.save()
            return Response(
                AccountSerializer(account).data, 
                status=status.HTTP_201_CREATED
            )
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class AccountDetailView(APIView):
    """Handle GET, PUT, DELETE for individual accounts"""
    
    def get(self, request, account_id):
        """Get account by id"""
        account = get_object_or_404(Account, id=account_id)
        serializer = AccountSerializer(account)
        return Response(serializer.data)
    
    def put(self, request, account_id):
        """Update account"""
        account = get_object_or_404(Account, id=account_id)
        
        if not request.data.get('password'):
            return Response(
                {"error": "Password must not be null"}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        serializer = AccountSerializer(account, data=request.data, partial=True)
        if serializer.is_valid():
            serializer.save()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    def delete(self, request, account_id):
        """Delete account"""
        account = get_object_or_404(Account, id=account_id)
        account.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


@api_view(['POST'])
def login_account(request, account_id):
    """Login account"""
    account = get_object_or_404(Account, id=account_id)
    serializer = LoginAccountSerializer(data=request.data)
    
    if serializer.is_valid():
        password = serializer.validated_data['password']
        if not password:
            return Response(
                {"error": "Password must not be null"}, 
                status=status.HTTP_400_BAD_REQUEST
            )
        
        hashed_password = Account.hash_password(password)
        if hashed_password == account.password:
            return Response(True)
        return Response(False)
    
    return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class WineListCreateView(APIView):
    """Handle GET (list all) and POST (create) for wines"""
    
    def get(self, request):
        """Get all wines"""
        wines = Wine.objects.all()
        serializer = WineSerializer(wines, many=True)
        return Response(serializer.data)
    
    def post(self, request):
        """Create a wine"""
        serializer = WineSerializer(data=request.data)
        if serializer.is_valid():
            wine = serializer.save()
            return Response(
                WineSerializer(wine).data, 
                status=status.HTTP_201_CREATED
            )
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class WineDetailView(APIView):
    """Handle GET, PUT, DELETE for individual wines"""
    
    def get(self, request, wine_id):
        """Get wine by id"""
        wine = get_object_or_404(Wine, id=wine_id)
        serializer = WineSerializer(wine)
        return Response(serializer.data)
    
    def put(self, request, wine_id):
        """Update wine"""
        wine = get_object_or_404(Wine, id=wine_id)
        serializer = WineSerializer(wine, data=request.data, partial=True)
        if serializer.is_valid():
            serializer.save()
            return Response(status=status.HTTP_204_NO_CONTENT)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    def delete(self, request, wine_id):
        """Delete wine"""
        wine = get_object_or_404(Wine, id=wine_id)
        wine.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


class CommentListCreateView(APIView):
    """Handle GET (list all) and POST (create) for comments"""
    
    def get(self, request):
        """Get all comments"""
        comments = Comment.objects.all()
        serializer = CommentSerializer(comments, many=True)
        return Response(serializer.data)
    
    def post(self, request):
        """Create a new comment"""
        serializer = CommentSerializer(data=request.data)
        if serializer.is_valid():
            comment = serializer.save()
            return Response(
                CommentSerializer(comment).data, 
                status=status.HTTP_201_CREATED
            )
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


class CommentDetailView(APIView):
    """Handle PUT, DELETE for individual comments"""
    
    def put(self, request, comment_id):
        """Update a comment"""
        comment = get_object_or_404(Comment, id=comment_id)
        serializer = UpdateCommentSerializer(data=request.data)
        
        if serializer.is_valid():
            if 'text' in serializer.validated_data:
                comment.text = serializer.validated_data['text']
                comment.save()
            return Response(status=status.HTTP_204_NO_CONTENT)
        
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    def delete(self, request, comment_id):
        """Delete a comment"""
        comment = get_object_or_404(Comment, id=comment_id)
        comment.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


@api_view(['GET'])
def get_comments_by_wine_id(request, wine_id):
    """Get comments by wine id"""
    comments = Comment.objects.filter(wine_id=wine_id)
    if not comments.exists():
        return Response(
            {"error": "No comments found for this wine."}, 
            status=status.HTTP_404_NOT_FOUND
        )
    serializer = CommentSerializer(comments, many=True)
    return Response(serializer.data)


@api_view(['GET'])
def get_comments_by_account_id(request, account_id):
    """Get comments by account id"""
    comments = Comment.objects.filter(account_id=account_id)
    if not comments.exists():
        return Response(
            {"error": "No comments found for this account."}, 
            status=status.HTTP_404_NOT_FOUND
        )
    serializer = CommentSerializer(comments, many=True)
    return Response(serializer.data)


class RatingListCreateUpdateView(APIView):
    """Handle GET (list all), POST (create), and PUT (update) for ratings"""
    
    def get(self, request):
        """Get all ratings"""
        ratings = Rating.objects.all()
        serializer = RatingSerializer(ratings, many=True)
        return Response(serializer.data)
    
    def post(self, request):
        """Create a new rating"""
        serializer = CreateRatingSerializer(data=request.data)
        if serializer.is_valid():
            rating = Rating.objects.create(**serializer.validated_data)
            return Response(
                RatingSerializer(rating).data, 
                status=status.HTTP_201_CREATED
            )
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    def put(self, request):
        """Update a rating"""
        serializer = UpdateRatingSerializer(data=request.data)
        if serializer.is_valid():
            rating_id = serializer.validated_data['id']
            rating = get_object_or_404(Rating, id=rating_id)
            
            if 'value' in serializer.validated_data:
                rating.value = serializer.validated_data['value']
                rating.save()
            
            return Response(status=status.HTTP_204_NO_CONTENT)
        
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


@api_view(['DELETE'])
def delete_rating(request, rating_id):
    """Delete a rating"""
    rating = get_object_or_404(Rating, id=rating_id)
    rating.delete()
    return Response(status=status.HTTP_204_NO_CONTENT)


@api_view(['GET'])
def get_ratings_by_wine_id(request, wine_id):
    """Get ratings by wine id"""
    ratings = Rating.objects.filter(wine_id=wine_id)
    if not ratings.exists():
        return Response(
            {"error": "No ratings found for this wine."}, 
            status=status.HTTP_404_NOT_FOUND
        )
    serializer = RatingSerializer(ratings, many=True)
    return Response(serializer.data)


@api_view(['GET'])
def get_ratings_by_account_id(request, account_id):
    """Get ratings by account id"""
    ratings = Rating.objects.filter(account_id=account_id)
    if not ratings.exists():
        return Response(
            {"error": "No ratings found for this account."}, 
            status=status.HTTP_404_NOT_FOUND
        )
    serializer = RatingSerializer(ratings, many=True)
    return Response(serializer.data)
