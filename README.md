# .NET Junior Developer Task
## Product Catalog Web Application
### Tech: .Net Core (MVC), MSSQL behind an ORM (EF is preferred)

### Description:
You are tasked with developing a product catalog system. This system features products that are displayed for a specified duration. There is a global list of products, and when the application loads, it retrieves the products meant to be shown at the current time. Products possess the following properties: a name, a creation date, created by userId, a start date, a duration, and a price.

Products are also categorized under predefined categories, which are seeded to the database and not added through the application interface.

Managing products (Add/Edit/Delete) is restricted to "Admin" users. User privileges are managed using Identity. It is crucial to consider security and proper error handling.

You must submit your work via Git.

### Application Features:

- View displaying all products regardless of the current time (Admins Only).
- Ability to add a new product (Admins Only).
- Ability to edit an existing product (Admins Only).
- Ability to delete an existing product (Admins Only).
- Ability to filter products by category.
- Details view for each product.

## Technologies, Packages, and Libraries Used in Solving the Task

### Frontend Technologies:
- **JavaScript:** Employed for creating dynamic and interactive web pages.
- **jQuery:** A widely-used JavaScript library for simplifying DOM manipulation and event handling.

### Frontend Libraries:
- **DataTables:** A jQuery plugin enhancing HTML tables with features like sorting, searching, and pagination.
- **SweetAlert2:** A beautiful and customizable replacement for JavaScript's native alert and confirm dialogs.
- **bootbox:** A small JavaScript library for creating programmatic dialog boxes.
- **Bootstrap:** A popular CSS framework for building responsive and visually appealing web designs.

### Backend Technologies:
- **SQL Server:** A robust relational database management system used for storing and managing data.
- **EF Core (Entity Framework Core):** An Object-Relational Mapping (ORM) framework for interacting with the database.
- **Serilog:** A versatile logging library for .NET applications.

### Backend Libraries:
- **AutoMapper:** A library for mapping objects between different layers of an application, enhancing data transfer and transformation.
- **HashidsNet:** Utilized for generating reversible and unique IDs from integers, enhancing data security and obfuscation.
- **UoN.ExpressiveAnnotations:** A library for adding validation rules to your models using annotations, ensuring data integrity and quality.
