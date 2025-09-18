from django.urls import path
from . import views

urlpatterns = [
    # Account endpoints - matching .NET API structure
    path('api/account/', views.AccountListCreateView.as_view(), name='account_list_create'),
    path('api/account/<uuid:account_id>/', views.AccountDetailView.as_view(), name='account_detail'),
    path('api/account/login/<uuid:account_id>/', views.login_account, name='login_account'),
    
    # Wine endpoints - matching .NET API structure
    path('api/wine/', views.WineListCreateView.as_view(), name='wine_list_create'),
    path('api/wine/<uuid:wine_id>/', views.WineDetailView.as_view(), name='wine_detail'),
    
    # Comment endpoints - matching .NET API structure
    path('api/comment/', views.CommentListCreateView.as_view(), name='comment_list_create'),
    path('api/comment/wine/<uuid:wine_id>/', views.get_comments_by_wine_id, name='get_comments_by_wine_id'),
    path('api/comment/account/<uuid:account_id>/', views.get_comments_by_account_id, name='get_comments_by_account_id'),
    path('api/comment/<uuid:comment_id>/', views.CommentDetailView.as_view(), name='comment_detail'),
    
    # Rating endpoints - matching .NET API structure
    path('api/rating/', views.RatingListCreateUpdateView.as_view(), name='rating_list_create_update'),
    path('api/rating/wine/<uuid:wine_id>/', views.get_ratings_by_wine_id, name='get_ratings_by_wine_id'),
    path('api/rating/account/<uuid:account_id>/', views.get_ratings_by_account_id, name='get_ratings_by_account_id'),
    path('api/rating/<uuid:rating_id>/', views.delete_rating, name='delete_rating'),
]