# Music Store - ASP.NET Core MVC E-Commerce Application

A collaborative full-stack music e-commerce web application built with ASP.NET Core MVC, Entity Framework Core, and SQLite. This project demonstrates team-based development using Git branching workflows, MVC architecture patterns, and modern C# web development practices.

## Project Overview

Music Store is a feature-rich online music store where users can browse albums by artists and genres, manage a shopping cart, and place orders. The application was developed by a team of 4 developers, each responsible for a distinct feature module:

- **Album Catalog** - Browse and filter albums by artist, genre, and search
- **Artist Management** - View artist profiles, biographies, and discographies
- **Shopping Cart** (My Contribution) - Session-based cart with full CRUD operations
- **Admin Dashboard** - Order management and store analytics

## Shopping Cart Feature (My Contribution)

I was responsible for designing and implementing the complete shopping cart system from scratch. This module handles all cart operations including adding items, updating quantities, removing items, and persisting cart data across user sessions.

### Key Features Implemented:

#### **Session-Based Cart Management**
- Implemented anonymous user tracking using GUID-based session IDs
- Cart automatically created on first add-to-cart action
- Session persists across page navigations and browser refreshes
- Each session maps to a unique cart stored in the database

#### **CRUD Operations**
```csharp
// Add album to cart - with duplicate prevention
GET /Cart/Add/{albumId}

// View cart contents
GET /Cart

// Update item quantity
GET /Cart/Update/{cartItemId}?quantity={newQuantity}

// Remove item from cart
GET /Cart/Remove/{cartItemId}
```

#### **Duplicate Item Prevention**
- When adding an album already in the cart, quantity increments instead of creating duplicate entries
- Ensures clean cart state and accurate inventory tracking

```csharp
var existingItem = cart.CartItems.FirstOrDefault(ci => ci.AlbumId == id);
if (existingItem != null) {
    existingItem.Quantity++;  // Increment existing item
} else {
    // Add new cart item
}
```

#### **Database Schema Design**
Designed a relational schema with proper Entity Framework navigation properties:

**Cart Model:**
```csharp
public class Cart {
    public int CartId { get; set; }
    public string SessionId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}
```

**CartItem Model:**
```csharp
public class CartItem {
    public int CartItemId { get; set; }
    public int CartId { get; set; }
    public int AlbumId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; }
    
    public virtual Cart Cart { get; set; }
    public virtual Album Album { get; set; }
}
```

#### **Eager Loading with EF Core**
Utilized efficient database queries with `Include()` and `ThenInclude()` to fetch related data:

```csharp
var cart = await _context.Carts
    .Include(c => c.CartItems)
    .ThenInclude(ci => ci.Album)
    .ThenInclude(a => a.Artist)
    .FirstOrDefaultAsync(c => c.SessionId == sessionId);
```

#### **Cart Persistence**
- Cart data saved to SQLite database for persistence across sessions
- LastUpdated timestamp tracks most recent cart modification
- Cart automatically retrieved on subsequent page visits using session ID

## Tech Stack

- **Backend:** ASP.NET Core MVC 8.0
- **ORM:** Entity Framework Core 8.0 (Code-First approach)
- **Database:** SQLite 3.x
- **Frontend:** Razor Views, Bootstrap 5, JavaScript
- **Version Control:** Git with feature branch workflow



3. **Database is pre-configured**
The SQLite database (`MusicStore.db`) is already included with sample data:
- 5 artists (The Beatles, Taylor Swift, Drake, Adele, Ed Sheeran)
- 5 genres (Rock, Pop, Hip Hop, R&B, Country)
- 10 albums with complete track listings

4. **Run the application**
```bash
dotnet run
```

5. **Navigate to the app**
Open your browser to `http://localhost:5000`


### Manual Testing Flow:
1. Browse to `/Store/Browse` and view albums
2. Click "View Details" on any album
3. Click "Add to Cart" button
4. View cart at `/Cart`
5. Use +/- buttons to adjust quantities
6. Click "Remove" to delete items
7. Verify cart persists after navigating away and returning

## 👥 Team Collaboration

This project was built using a Git branching strategy:

- **Main branch:** Production-ready code
- **Develop branch:** Integration branch for all features
- **Feature branches:** Individual features developed in isolation

### My Workflow:
```bash
# Create and work on cart feature branch
git checkout -b feature/shopping-cart
git add .
git commit -m "Implemented shopping cart with session management"
git push origin feature/shopping-cart

# Merge to develop after review
git checkout develop
git pull origin develop
git merge feature/shopping-cart
git push origin develop
```

## Key Learnings & Accomplishments

### Technical Skills Demonstrated:
- ✅ ASP.NET Core MVC architecture and routing
- ✅ Entity Framework Core with navigation properties and eager loading
- ✅ Session management for anonymous users
- ✅ LINQ queries for data manipulation
- ✅ Razor view engine and Bootstrap UI development
- ✅ Git branching workflows and team collaboration
- ✅ Database schema design with foreign key relationships
- ✅ RESTful routing conventions




## 📄 License

This project was developed as part of HTTP5226 - Web Development coursework at Humber College.

---


