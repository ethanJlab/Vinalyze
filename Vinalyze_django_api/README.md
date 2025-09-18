# Vinalyze Django API

This is a Python Django REST API that replaces the original .NET API for the Vinalyze wine tracking application.

## Features

- Complete RESTful API matching the original .NET implementation
- Account management with password hashing (SHA256)
- Wine CRUD operations
- Comment system for wines
- Rating system (1-5 scale)
- Admin interface for data management
- Comprehensive test suite

## Models

### Account
- `id`: UUID (primary key)
- `username`: String
- `password`: String (SHA256 hashed)
- `email`: Email
- `liked_wines`: JSON array of wine UUIDs

### Wine
- `id`: UUID (primary key)
- `name`: String
- `description`: Text
- `flavor_profile`: Text

### Comment
- `id`: UUID (primary key)
- `account_id`: UUID (foreign key reference)
- `wine_id`: UUID (foreign key reference)
- `text`: Text

### Rating
- `id`: UUID (primary key)
- `account_id`: UUID (foreign key reference)
- `wine_id`: UUID (foreign key reference)
- `value`: Integer (1-5)

## API Endpoints

### Account Endpoints
- `GET /api/account/` - List all accounts
- `POST /api/account/` - Create new account
- `GET /api/account/{id}/` - Get account by ID
- `PUT /api/account/{id}/` - Update account
- `DELETE /api/account/{id}/` - Delete account
- `POST /api/account/login/{id}/` - Login account

### Wine Endpoints
- `GET /api/wine/` - List all wines
- `POST /api/wine/` - Create new wine
- `GET /api/wine/{id}/` - Get wine by ID
- `PUT /api/wine/{id}/` - Update wine
- `DELETE /api/wine/{id}/` - Delete wine

### Comment Endpoints
- `GET /api/comment/` - List all comments
- `POST /api/comment/` - Create new comment
- `GET /api/comment/wine/{wine_id}/` - Get comments by wine
- `GET /api/comment/account/{account_id}/` - Get comments by account
- `PUT /api/comment/{id}/` - Update comment
- `DELETE /api/comment/{id}/` - Delete comment

### Rating Endpoints
- `GET /api/rating/` - List all ratings
- `POST /api/rating/` - Create new rating
- `PUT /api/rating/` - Update rating (requires rating ID in body)
- `GET /api/rating/wine/{wine_id}/` - Get ratings by wine
- `GET /api/rating/account/{account_id}/` - Get ratings by account
- `DELETE /api/rating/{id}/` - Delete rating

## Setup and Installation

1. Install dependencies:
   ```bash
   pip install -r requirements.txt
   ```

2. Run migrations:
   ```bash
   python manage.py migrate
   ```

3. Create seed data:
   ```bash
   python manage.py populate_seed_data
   ```

4. Run the development server:
   ```bash
   python manage.py runserver
   ```

5. (Optional) Create a superuser for admin access:
   ```bash
   python manage.py createsuperuser
   ```

## Testing

Run the test suite:
```bash
python manage.py test
```

## Admin Interface

Access the Django admin at `/admin/` to manage data through a web interface.

## Seed Data

The application comes with pre-populated seed data:
- Admin account (username: "admin", password: "admin")
- Sample wine
- Sample comment and rating

## Differences from .NET API

- Uses UUIDs instead of GUIDs (same format, different terminology)
- JSON responses instead of XML support
- Django's built-in admin interface
- Python-style naming conventions in code (but API endpoints match exactly)
- SQLite database by default (easily configurable for other databases)

## Configuration

The API is configured for development with:
- CORS enabled for all origins
- Debug mode enabled
- SQLite database
- JSON-only API responses

For production deployment, update `settings.py` to:
- Disable debug mode
- Configure proper database
- Set secure CORS origins
- Configure static files serving