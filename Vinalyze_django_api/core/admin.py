from django.contrib import admin
from .models import Account, Wine, Comment, Rating


@admin.register(Account)
class AccountAdmin(admin.ModelAdmin):
    list_display = ['username', 'email', 'id']
    list_filter = ['email']
    search_fields = ['username', 'email']
    readonly_fields = ['id']


@admin.register(Wine)
class WineAdmin(admin.ModelAdmin):
    list_display = ['name', 'id']
    search_fields = ['name', 'description']
    readonly_fields = ['id']


@admin.register(Comment)
class CommentAdmin(admin.ModelAdmin):
    list_display = ['account_id', 'wine_id', 'text', 'id']
    list_filter = ['account_id', 'wine_id']
    readonly_fields = ['id']


@admin.register(Rating)
class RatingAdmin(admin.ModelAdmin):
    list_display = ['account_id', 'wine_id', 'value', 'id']
    list_filter = ['value', 'account_id', 'wine_id']
    readonly_fields = ['id']
