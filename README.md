# SOLID Principles Demo Project

## Overview
This repository demonstrates the five **SOLID principles** of object-oriented programming and design. These principles were introduced by Robert C. Martin (Uncle Bob) to make software designs more understandable, flexible, and maintainable.

## What are SOLID Principles?

**SOLID** is an acronym representing five design principles:
- **S** - Single Responsibility Principle (SRP)
- **O** - Open/Closed Principle (OCP)
- **L** - Liskov Substitution Principle (LSP)
- **I** - Interface Segregation Principle (ISP)
- **D** - Dependency Inversion Principle (DIP)

---

## 1. Single Responsibility Principle (SRP)

### Definition
> A class should have one, and only one, reason to change.

### Explanation
Each class should focus on a single responsibility or functionality. This makes the code:
- Easier to understand and maintain
- Less prone to bugs when changes are needed
- More reusable

### Example - WRONG ❌
```csharp
// This class has too many responsibilities
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    // Responsibility 1: User data validation
    public bool ValidateEmail()
    {
        return Email.Contains("@");
    }
    
    // Responsibility 2: Database operations
    public void SaveToDatabase()
    {
        // Database save logic
        Console.WriteLine("Saving user to database...");
    }
    
    // Responsibility 3: Email notifications
    public void SendWelcomeEmail()
    {
        // Email sending logic
        Console.WriteLine($"Sending welcome email to {Email}");
    }
}
```

### Example - CORRECT ✅
```csharp
// Each class has a single responsibility
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserValidator
{
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}

public class UserRepository
{
    public void Save(User user)
    {
        // Database save logic
        Console.WriteLine($"Saving user {user.Name} to database...");
    }
}

public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
        // Email sending logic
        Console.WriteLine($"Sending welcome email to {email}");
    }
}
```

---

## 2. Open/Closed Principle (OCP)

### Definition
> Software entities should be open for extension but closed for modification.

### Explanation
You should be able to add new functionality without changing existing code. This is typically achieved through:
- Abstraction (interfaces/abstract classes)
- Inheritance
- Polymorphism

### Example - WRONG ❌
```csharp
public class AreaCalculator
{
    public double CalculateArea(object shape)
    {
        if (shape is Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }
        else if (shape is Circle circle)
        {
            return Math.PI * circle.Radius * circle.Radius;
        }
        // Need to modify this method every time we add a new shape!
        return 0;
    }
}

public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
}

public class Circle
{
    public double Radius { get; set; }
}
```

### Example - CORRECT ✅
```csharp
// Abstraction that is closed for modification
public interface IShape
{
    double CalculateArea();
}

// Open for extension - can add new shapes without modifying existing code
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea()
    {
        return Width * Height;
    }
}

public class Circle : IShape
{
    public double Radius { get; set; }
    
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }
    
    public double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
}

public class AreaCalculator
{
    public double CalculateArea(IShape shape)
    {
        // No modification needed when new shapes are added!
        return shape.CalculateArea();
    }
}
```

---

## 3. Liskov Substitution Principle (LSP)

### Definition
> Objects of a superclass should be replaceable with objects of a subclass without breaking the application.

### Explanation
Derived classes must be substitutable for their base classes. This means:
- Subclasses should extend functionality, not replace it
- Method signatures should be compatible
- No unexpected behavior when using polymorphism

### Example - WRONG ❌
```csharp
public class Bird
{
    public virtual void Fly()
    {
        Console.WriteLine("Flying...");
    }
}

public class Sparrow : Bird
{
    public override void Fly()
    {
        Console.WriteLine("Sparrow flying...");
    }
}

// Violates LSP - Penguin can't fly but inherits from Bird
public class Penguin : Bird
{
    public override void Fly()
    {
        throw new NotImplementedException("Penguins can't fly!");
    }
}

// This will break when passed a Penguin
public void MakeBirdFly(Bird bird)
{
    bird.Fly(); // Exception if bird is a Penguin!
}
```

### Example - CORRECT ✅
```csharp
// Better abstraction
public abstract class Bird
{
    public abstract void Move();
}

public interface IFlyable
{
    void Fly();
}

public class Sparrow : Bird, IFlyable
{
    public override void Move()
    {
        Fly();
    }
    
    public void Fly()
    {
        Console.WriteLine("Sparrow flying...");
    }
}

public class Penguin : Bird
{
    public override void Move()
    {
        Swim();
    }
    
    public void Swim()
    {
        Console.WriteLine("Penguin swimming...");
    }
}

// Now we can safely work with different behaviors
public void MakeBirdMove(Bird bird)
{
    bird.Move(); // Works for all birds!
}

public void MakeFlyableFly(IFlyable flyable)
{
    flyable.Fly(); // Only accepts birds that can fly
}
```

---

## 4. Interface Segregation Principle (ISP)

### Definition
> Clients should not be forced to depend on interfaces they do not use.

### Explanation
Create specific, focused interfaces rather than one large, general-purpose interface. This:
- Prevents classes from implementing methods they don't need
- Makes the system more decoupled and easier to refactor
- Follows the principle of "many small interfaces over one big interface"

### Example - WRONG ❌
```csharp
// Fat interface with too many responsibilities
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
    void GetPaid();
}

// Human worker can implement all methods
public class HumanWorker : IWorker
{
    public void Work() { Console.WriteLine("Working..."); }
    public void Eat() { Console.WriteLine("Eating..."); }
    public void Sleep() { Console.WriteLine("Sleeping..."); }
    public void GetPaid() { Console.WriteLine("Getting paid..."); }
}

// Robot worker doesn't need Eat, Sleep, or GetPaid
public class RobotWorker : IWorker
{
    public void Work() { Console.WriteLine("Working..."); }
    public void Eat() { throw new NotImplementedException(); }
    public void Sleep() { throw new NotImplementedException(); }
    public void GetPaid() { throw new NotImplementedException(); }
}
```

### Example - CORRECT ✅
```csharp
// Segregated interfaces - each focused on specific behavior
public interface IWorkable
{
    void Work();
}

public interface IFeedable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public interface IPayable
{
    void GetPaid();
}

// Human implements all relevant interfaces
public class HumanWorker : IWorkable, IFeedable, ISleepable, IPayable
{
    public void Work() { Console.WriteLine("Working..."); }
    public void Eat() { Console.WriteLine("Eating..."); }
    public void Sleep() { Console.WriteLine("Sleeping..."); }
    public void GetPaid() { Console.WriteLine("Getting paid..."); }
}

// Robot only implements what it needs
public class RobotWorker : IWorkable
{
    public void Work() { Console.WriteLine("Robot working..."); }
}

// Manager can manage any workable entity
public class WorkManager
{
    public void ManageWork(IWorkable worker)
    {
        worker.Work();
    }
}
```

---

## 5. Dependency Inversion Principle (DIP)

### Definition
> High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should depend on abstractions.

### Explanation
- Depend on interfaces/abstractions, not concrete implementations
- This makes the code more flexible and testable
- Allows easy swapping of implementations
- Reduces coupling between modules

### Example - WRONG ❌
```csharp
// Low-level module
public class EmailService
{
    public void SendEmail(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}

// High-level module depends directly on low-level module
public class Notification
{
    private EmailService _emailService;
    
    public Notification()
    {
        // Tightly coupled to EmailService
        _emailService = new EmailService();
    }
    
    public void Send(string message)
    {
        _emailService.SendEmail(message);
    }
}

// Cannot easily switch to SMS or other notification types
// Hard to test because EmailService is created internally
```

### Example - CORRECT ✅
```csharp
// Abstraction
public interface IMessageService
{
    void SendMessage(string message);
}

// Low-level modules implement the abstraction
public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}

public class SmsService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

public class PushNotificationService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending push notification: {message}");
    }
}

// High-level module depends on abstraction
public class Notification
{
    private IMessageService _messageService;
    
    // Dependency is injected (Dependency Injection)
    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public void Send(string message)
    {
        _messageService.SendMessage(message);
    }
}

// Usage - easy to switch implementations
public class Program
{
    public static void Main()
    {
        // Can easily switch between different services
        var emailNotification = new Notification(new EmailService());
        emailNotification.Send("Hello via Email");
        
        var smsNotification = new Notification(new SmsService());
        smsNotification.Send("Hello via SMS");
        
        var pushNotification = new Notification(new PushNotificationService());
        pushNotification.Send("Hello via Push");
    }
}
```

---

## Benefits of Following SOLID Principles

1. **Maintainability**: Code is easier to understand and modify
2. **Testability**: Components are loosely coupled and easy to test
3. **Scalability**: New features can be added with minimal changes to existing code
4. **Reusability**: Well-designed components can be reused across projects
5. **Flexibility**: Easy to adapt to changing requirements
6. **Reduced Bugs**: Better structure leads to fewer errors

## When to Apply SOLID Principles

- When designing new systems or components
- When refactoring existing code
- When code becomes difficult to maintain or extend
- When preparing for future changes or growth

## Important Notes

- Don't over-engineer: Apply these principles pragmatically
- Balance is key: Sometimes a simple solution is better than a perfectly SOLID one
- These principles work together: Often applying one helps with others
- Practice makes perfect: The more you use them, the more natural they become

## Resources

- **Clean Code** by Robert C. Martin
- **Agile Software Development, Principles, Patterns, and Practices** by Robert C. Martin
- **Design Patterns: Elements of Reusable Object-Oriented Software** by Gang of Four

---

## Contributing

Feel free to contribute more examples or improvements to this repository!

## License

This project is for educational purposes.
