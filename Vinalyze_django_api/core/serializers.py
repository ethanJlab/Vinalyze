from rest_framework import serializers
from .models import Account, Wine, Comment, Rating


class AccountSerializer(serializers.ModelSerializer):
    class Meta:
        model = Account
        fields = ['id', 'username', 'password', 'email', 'liked_wines']
        extra_kwargs = {
            'password': {'write_only': True}
        }

    def create(self, validated_data):
        # Password will be hashed in the model's save method
        return Account.objects.create(**validated_data)

    def update(self, instance, validated_data):
        # Handle password hashing on update
        if 'password' in validated_data:
            instance.password = validated_data.pop('password')
        for attr, value in validated_data.items():
            setattr(instance, attr, value)
        instance.save()
        return instance


class WineSerializer(serializers.ModelSerializer):
    class Meta:
        model = Wine
        fields = ['id', 'name', 'description', 'flavor_profile']


class CommentSerializer(serializers.ModelSerializer):
    class Meta:
        model = Comment
        fields = ['id', 'account_id', 'wine_id', 'text']


class RatingSerializer(serializers.ModelSerializer):
    class Meta:
        model = Rating
        fields = ['id', 'account_id', 'wine_id', 'value']


# Custom serializers for specific API endpoints
class CreateRatingSerializer(serializers.Serializer):
    account_id = serializers.UUIDField()
    wine_id = serializers.UUIDField()
    value = serializers.IntegerField(min_value=1, max_value=5)


class UpdateRatingSerializer(serializers.Serializer):
    id = serializers.UUIDField()
    value = serializers.IntegerField(min_value=1, max_value=5, required=False)


class UpdateCommentSerializer(serializers.Serializer):
    text = serializers.CharField(required=False, allow_blank=True)


class WineToLikesRequestSerializer(serializers.Serializer):
    account_id = serializers.UUIDField()
    wine_id = serializers.UUIDField()


class LoginAccountSerializer(serializers.Serializer):
    password = serializers.CharField()