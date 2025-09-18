import uuid
from django.test import TestCase
from django.urls import reverse
from rest_framework.test import APITestCase
from rest_framework import status
from .models import Account, Wine, Comment, Rating


class AccountModelTest(TestCase):
    def test_password_hashing(self):
        """Test that passwords are properly hashed"""
        account = Account.objects.create(
            username="testuser",
            password="testpass",
            email="test@example.com"
        )
        # Password should be hashed (64 char hex string)
        self.assertEqual(len(account.password), 64)
        self.assertNotEqual(account.password, "testpass")
    
    def test_hash_password_static_method(self):
        """Test the static hash_password method"""
        hashed = Account.hash_password("testpass")
        self.assertEqual(len(hashed), 64)
        # Should be consistent
        self.assertEqual(hashed, Account.hash_password("testpass"))


class AccountAPITest(APITestCase):
    def setUp(self):
        self.account = Account.objects.create(
            username="testuser",
            password="testpass",
            email="test@example.com"
        )
    
    def test_get_all_accounts(self):
        """Test GET /api/account/"""
        url = reverse('account_list_create')
        response = self.client.get(url)
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertEqual(len(response.data), 1)
    
    def test_get_account_by_id(self):
        """Test GET /api/account/<id>/"""
        url = reverse('account_detail', kwargs={'account_id': self.account.id})
        response = self.client.get(url)
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertEqual(response.data['username'], 'testuser')
    
    def test_create_account(self):
        """Test POST /api/account/"""
        url = reverse('account_list_create')
        data = {
            'username': 'newuser',
            'password': 'newpass',
            'email': 'newuser@example.com'
        }
        response = self.client.post(url, data, format='json')
        self.assertEqual(response.status_code, status.HTTP_201_CREATED)
        self.assertEqual(Account.objects.count(), 2)
    
    def test_login_success(self):
        """Test successful login"""
        url = reverse('login_account', kwargs={'account_id': self.account.id})
        data = {'password': 'testpass'}
        response = self.client.post(url, data, format='json')
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertTrue(response.data)
    
    def test_login_failure(self):
        """Test failed login with wrong password"""
        url = reverse('login_account', kwargs={'account_id': self.account.id})
        data = {'password': 'wrongpass'}
        response = self.client.post(url, data, format='json')
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertFalse(response.data)


class WineAPITest(APITestCase):
    def setUp(self):
        self.wine = Wine.objects.create(
            name="Test Wine",
            description="A test wine",
            flavor_profile="Fruity"
        )
    
    def test_get_all_wines(self):
        """Test GET /api/wine/"""
        url = reverse('wine_list_create')
        response = self.client.get(url)
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertEqual(len(response.data), 1)
    
    def test_create_wine(self):
        """Test POST /api/wine/"""
        url = reverse('wine_list_create')
        data = {
            'name': 'New Wine',
            'description': 'A new test wine',
            'flavor_profile': 'Bold and complex'
        }
        response = self.client.post(url, data, format='json')
        self.assertEqual(response.status_code, status.HTTP_201_CREATED)
        self.assertEqual(Wine.objects.count(), 2)


class RatingAPITest(APITestCase):
    def setUp(self):
        self.account = Account.objects.create(
            username="testuser",
            password="testpass",
            email="test@example.com"
        )
        self.wine = Wine.objects.create(
            name="Test Wine",
            description="A test wine",
            flavor_profile="Fruity"
        )
    
    def test_create_rating(self):
        """Test POST /api/rating/"""
        url = reverse('rating_list_create_update')
        data = {
            'account_id': str(self.account.id),
            'wine_id': str(self.wine.id),
            'value': 5
        }
        response = self.client.post(url, data, format='json')
        self.assertEqual(response.status_code, status.HTTP_201_CREATED)
        self.assertEqual(Rating.objects.count(), 1)
    
    def test_get_ratings_by_wine(self):
        """Test GET /api/rating/wine/<wine_id>/"""
        rating = Rating.objects.create(
            account_id=self.account.id,
            wine_id=self.wine.id,
            value=4
        )
        url = reverse('get_ratings_by_wine_id', kwargs={'wine_id': self.wine.id})
        response = self.client.get(url)
        self.assertEqual(response.status_code, status.HTTP_200_OK)
        self.assertEqual(len(response.data), 1)
        self.assertEqual(response.data[0]['value'], 4)
