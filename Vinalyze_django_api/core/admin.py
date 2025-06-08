from django.contrib import admin
from .models import *

# Register your models here.
admin.site.register([Wine,Account,Rating,Comment])
