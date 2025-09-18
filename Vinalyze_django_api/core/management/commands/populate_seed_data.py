import uuid
from django.core.management.base import BaseCommand
from core.models import Account, Wine, Comment, Rating


class Command(BaseCommand):
    help = 'Populate database with seed data matching .NET version'

    def handle(self, *args, **options):
        # Create seed data matching the .NET version
        
        # Admin account with pre-defined UUID
        admin_id = uuid.UUID("0f8fad5b-d9cb-469f-a165-70867728950e")
        wine_id = uuid.UUID("7c9e6679-7425-40de-944b-e07fc1f90ae7")
        comment_id = uuid.UUID("d1f8b2c3-4e5f-6a7b-8c9d-e0f1a2b3c4d5")
        rating_id = uuid.UUID("e1f2a3b4-c5d6-7e8f-9a0b-1c2d3e4f5a6b")
        
        # Create admin account
        account, created = Account.objects.get_or_create(
            id=admin_id,
            defaults={
                'username': 'admin',
                'password': Account.hash_password('admin'),
                'email': 'admin@gmail.com',
                'liked_wines': [str(wine_id)]
            }
        )
        if created:
            self.stdout.write(
                self.style.SUCCESS('Successfully created admin account')
            )
        else:
            self.stdout.write('Admin account already exists')
        
        # Create sample wine
        wine, created = Wine.objects.get_or_create(
            id=wine_id,
            defaults={
                'name': 'Sample Wine',
                'description': 'This was created from a plant somewhere on planet Earth. It is somewhere between 1 day old and 100 years old.',
                'flavor_profile': 'This tastes like dirt and feet.'
            }
        )
        if created:
            self.stdout.write(
                self.style.SUCCESS('Successfully created sample wine')
            )
        else:
            self.stdout.write('Sample wine already exists')
        
        # Create sample comment
        comment, created = Comment.objects.get_or_create(
            id=comment_id,
            defaults={
                'account_id': admin_id,
                'wine_id': wine_id,
                'text': 'This wine is amazing! I love the flavor profile.'
            }
        )
        if created:
            self.stdout.write(
                self.style.SUCCESS('Successfully created sample comment')
            )
        else:
            self.stdout.write('Sample comment already exists')
        
        # Create sample rating
        rating, created = Rating.objects.get_or_create(
            id=rating_id,
            defaults={
                'account_id': admin_id,
                'wine_id': wine_id,
                'value': 5
            }
        )
        if created:
            self.stdout.write(
                self.style.SUCCESS('Successfully created sample rating')
            )
        else:
            self.stdout.write('Sample rating already exists')
        
        self.stdout.write(
            self.style.SUCCESS('Seed data population completed!')
        )