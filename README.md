# LoxLang

# Features
Lox is a simple yet powerful programming language that supports:

- Dynamic Typing: Variables can hold any type of value.
- First-Class Functions: Functions are first-class citizens, meaning they can be assigned to variables, passed as arguments, and returned from other functions.
- Closures: Functions retain access to variables from their defining environment, even after that environment has gone out of scope.
- Object-Oriented Programming:
     - Classes and instances.
     - Inheritance for sharing behavior.
- Control Flow:
- if and else for conditional logic.
- while loops for iteration.
- for loops for concise iteration.
- Error Handling: Graceful error reporting and runtime exceptions.

## Lox Syntax

- Hello, World
```
print "Hello, World!";
```

- Variables
```
var name = "Lox";
var year = 2023;
print "Welcome to " + name + "!"; // Output: Welcome to Lox!
```

- Functions
```
fun greet(name) {
  print "Hello, " + name + "!";
}

greet("World"); // Output: Hello, World!
```

- Closures
  
```
fun makeCounter() {
  var count = 0;
  fun increment() {
    count = count + 1;
    return count;
  }
  return increment;
}

var counter = makeCounter();
print counter(); // 1
print counter(); // 2
```

- Classes and Objects

```
class Person {
  init(name) {
    this.name = name;
  }

  sayHello() {
    print "Hello, my name is " + this.name + ".";
  }
}

var john = Person("John");
john.sayHello(); // Output: Hello, my name is John.
```

- Inheritance
```
class Animal {
  speak() {
    print "I am an animal.";
  }
}

class Dog < Animal {
  speak() {
    print "Woof!";
  }
}

var dog = Dog();
dog.speak(); // Output: Woof!
```











