import uuid
import hashlib
from django.db import models
from django.core.validators import MinValueValidator, MaxValueValidator


class Account(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    username = models.CharField(max_length=255, blank=True, null=True)
    password = models.CharField(max_length=255, blank=True, null=True)
    email = models.EmailField(blank=True, null=True)
    liked_wines = models.JSONField(default=list, blank=True)  # List of wine UUIDs

    def save(self, *args, **kwargs):
        # Hash password if it's being set and not already hashed (64 char hex string)
        if self.password and len(self.password) != 64:
            self.password = self.hash_password(self.password)
        super().save(*args, **kwargs)

    @staticmethod
    def hash_password(password):
        """Hash password using SHA256 to match .NET implementation"""
        if password:
            return hashlib.sha256(password.encode('utf-8')).hexdigest()
        return password

    def __str__(self):
        return self.username or str(self.id)


class Wine(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    name = models.CharField(max_length=255, blank=True, null=True)
    description = models.TextField(blank=True, null=True)
    flavor_profile = models.TextField(blank=True, null=True)

    def __str__(self):
        return self.name or str(self.id)


class Comment(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    account_id = models.UUIDField()
    wine_id = models.UUIDField()
    text = models.TextField(blank=True, null=True)

    def __str__(self):
        return f"Comment by {self.account_id} on {self.wine_id}"


class Rating(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    account_id = models.UUIDField()
    wine_id = models.UUIDField()
    value = models.IntegerField(
        validators=[MinValueValidator(1), MaxValueValidator(5)]
    )

    def __str__(self):
        return f"Rating {self.value} by {self.account_id} on {self.wine_id}"
