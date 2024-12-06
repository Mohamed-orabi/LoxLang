# Lox Programming Language

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

- If
```
if (condition) {
  print "yes";
} else {
  print "no";
}
```

- While
```
var a = 1;
while (a < 10) {
  print a;
  a = a + 1;
}
```

- For
  
```
for (var a = 1; a < 10; a = a + 1) {
  print a;
}
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
class Bacon {
  eat() {
    print "Crunch crunch crunch!";
  }
}

Bacon().eat(); // Prints "Crunch crunch crunch!".
```













