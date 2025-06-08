from django.db import models
from django.core.validators import validate_email, MaxValueValidator, MinValueValidator

import uuid

# Create your models here.

# Base ORM for the Wine entity
class Wine(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    Name = models.CharField(max_length=200)
    Description = models.CharField()
    FlavorProfile = models.CharField()

# Base ORM for the user Account entity
class Account(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    UserName = models.CharField(max_length=200)
    Password = models.CharField(max_length=200)
    Email = models.CharField(validators = [validate_email])
    LikedWines = models.ForeignKey(Wine)
    
    
# Base ORM for the Wine Rating entity
class Rating(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    AccountId = models.ForeignKey(Account)
    WineId = models.ForeignKey(Wine)
    Value = models.IntegerField(MaxValueValidator(5), MinValueValidator(0))

# Base ORm for the Comment entity
class Comment(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    AccountId = models.ForeignKey(Account)
    WineId = models.ForeignKey(Wine)
    Text = models.CharField()

