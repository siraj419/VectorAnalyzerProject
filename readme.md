# **Documentation for Vector Analyzer Project**

## **Overview**

The **Vector Analyzer** is a simple command-line program made in C# for handling and working with 3D vectors. With this app, you can create, store, and perform different mathematical operations on vectors. The project is organized in a way that separates vector data, storage management, and calculations.


## **Getting Started**

### **1. Requirements**

* Visual Studio 2012 or later installed.
* .NET Framework 4.0 or higher.

### **2. How to Run**

1. Clone the repository or copy the source code.
2. Open Visual Studio and create a new Console Application project.
3. Paste the source code into the Program.cs file.
4. Fix any errors shown in Visual Studio.
5. Run the program using F5 or by selecting “Start Debugging” from the menu.
6. Use the command-line interface to interact with the app.



## **Key Components**

### **1. Vector Class**

* Represents a 3D vector with components **`x`**, **`y`**, and **`z`**.
* **Constructor:**
  * **`Vector(string name, double x, double y, double z)`**
* **Methods:**
  * **`double GetMagnitude()`**: Calculates the vector's magnitude.
  * **`Vector GetUnitVector()`**: Gives a unit vector (magnitude = 1).
  * **`static Vector Parse(string input)`**: Creates a vector from an input string like “v = 3i + 4j - 2k”.

### **2. Store Class**

* Manages a collection of vectors.
* **Constructor:**
  * **`Store(uint size)`**
* **Methods:**
  * **`bool AddVector(Vector v)`**: Adds a new vector to the store.
  * **`bool RemoveVector(string name)`**: Removes a vector using its name.
  * **`void ListVectors()`**: Shows all stored vectors.
  * **`Vector GetVector(string name)`**: Retrieves a vector by name.



### **3. VectorOperations Class**

* Contains static methods for performing mathematical operations.
* **Methods:**
  * **`static Vector AddVectors(string name, Vector v1, Vector v2)`**: Adds two vectors.
  * **`static Vector SubtractVectors(string name, Vector v1, Vector v2)`**: Subtracts one vector from another.
  * **`static Vector ScalarMultiplication(Vector v, double scalar)`**: Multiplies a vector by a scalar.
  * **`static double DotProduct(Vector v1, Vector v2)`**: Finds the dot product of two vectors.
  * **`static Vector CrossProduct(Vector v1, Vector v2)`**: Calculates the cross product.
  * **`static double Projection(Vector ofVector, Vector overVector)`**:  Gives the projection of one vector on another.
  *  **`static double GetAngleBetweenVectors(Vector v1, Vector v2)`**: Finds the angle between two vectors in degrees.



### **4. VectorAnalyzer Class**

* Provides the main menu and coordinates user interactions.
* **Menu Options:**
  * Add, Remove, and Modify Vectors in the Store
  * Perform Addition, Subtraction, Scalar Multiplication
  * Calculate Dot Product, Cross Product, and Angles



## **How to Use**

### **1. Adding a Vector**
Type your vector like this:


 **`v = 2i + 3j + 4k`**


### **2. Performing Operations**

* Navigate through the menu to perform tasks like:
  * Add or remove vectors.
  * Find dot or cross products.
  * Compute unit vectors, angles, or projections.

### **3. Example Session**

```
--------------------------------------------------------
                        MAIN MENU
--------------------------------------------------------
0. Exit
1. Vectors Store (To add, remove, modify, see vectors)  
2. Addition of Vectors
3. Subtraction of Vectors
4. Scalar Product
5. Dot Product
6. Cross Product
7. Calculate Angle Between Two Vectors
8. Projection
9. Redisplay Menu

9 to Redisplay Menu
Select an Option: 1
Welcome to Store

-----------------------------------
          VECTOR STORE MENU
-----------------------------------
0. Exit form Store
1. Add a new vector
2. List Vectors
3. Remove a vector
4. Modify a vector
5. Explore a Vector
6. Display Unit Vector
7. Redisplay Store Menu

7 to Redisplay Menu
Select an Option(Store): 1

Input a vector along with its name (e.g. v = 2i + 3j - 10k):
v = 2i + 3j + 4k     
Vector added successfully
```


## **Error Handling**

* Invalid input is rejected with error messages.
* Operations involving zero vectors (like finding angles) are not allowed.
* You can store up to 20 vectors only.


## **Future Plans**

* Add file input/output for saving vectors.
* Support n-dimensional vectors.
* Develop a **Graphical User Interface (GUI)**.



## **Contributors**
  * **Siraj Shabbir**
  * **Muhammad Farhan Ali**