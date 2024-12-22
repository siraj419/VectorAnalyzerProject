class Vector
{

    public Vector GetUnitVector()
    {
        double magnitude = GetMagnitude();
        if (magnitude == 0) throw new InvalidOperationException("Cannot create a unit vector for a zero vector.");
        return new Vector(name + "_unit", x / magnitude, y / magnitude, z / magnitude);
    }

    public static double GetAngleBetweenVectors(Vector v1, Vector v2)
    {
        double dotProduct = VectorOperations.DotProduct(v1, v2);
        double magnitudes = v1.GetMagnitude() * v2.GetMagnitude();
        if (magnitudes == 0)
        {
            Console.WriteLine("Cannot calculate angle between vectors if one has zero magnitude.");
            return double.NaN;
        }
        return Math.Acos(dotProduct / magnitudes) * (180 / Math.PI); // Angle in degrees
    }  

    //Siraaj
    // Vector Components 
    double x;
    double y;
    double z;
    // Vector Name
    string name;

    public Vector(string name, double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.name = name;
    }

    public double GetMagnitude()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    // Getter Methods
    public double GetXComponent()
    {
        return x;
    }

    public double GetYComponent()
    {
        return y;
    }
    public double GetZComponent()
    {
        return z;
    }

    public string GetName()
    {
        return name;
    }

    // String Parser Static Method
    public static Vector Parse(string stringVector)
    {
        // Removing extra spaces and converting to lowercase
        stringVector = stringVector.Replace(" ", "").ToLower();
        string name, vectorPart;
        try
        {
            // Converting input vector into two parts (vectorName and Vector)
            string[] parts = stringVector.Split("=");
            name = parts[0];
            vectorPart = parts[1];
        }
        catch
        {
            return null;
        }


        // Initializing Parser Variables 
        sbyte sign = 1;
        bool afterPoint = false;
        bool hasCoff = false;
        double beforePointNumber = 0;
        double afterPointNumber = 0;
        double powerOfTen = 10;
        double x, y, z;
        x = y = z = 0;

        foreach (char ch in vectorPart)
        {
          switch (ch)
          {
            case '-':
              sign = -1;
              break;

            case '+':
              sign = 1;
              break;

            case '.':
              afterPoint = true;
              break;

            case 'i':
            case 'j':
            case 'k':
              double result = (beforePointNumber + afterPointNumber) * sign;
              if (!hasCoff)
                result = (result == 0) ? sign : result;
              // Resetting the Variables
              beforePointNumber = 0;
              afterPointNumber = 0;
              afterPoint = false;
              powerOfTen = 10;
              hasCoff = false;

              if (ch == 'i') x = result;
              else if (ch == 'j') y = result;
              else z = result;

              break;

            default:
              if (!char.IsDigit(ch))
                return null;
              hasCoff = true;
              
              if (afterPoint)
              {
                  afterPointNumber += (ch - '0') / powerOfTen;
                  powerOfTen *= 10;
                  break;
              }

              beforePointNumber = beforePointNumber * 10 + (ch - '0');
              break;
            }
        }

        return new Vector(name, x, y, z);
    }

}

class Store
{
    uint STORE_SIZE;
    Vector[] vectors;
    uint counter = 0;

    public Store(uint size)
    {
        STORE_SIZE = size;
        vectors = new Vector[size];
    }

    void DisplayVectorsStoreMenu()
    {
        Console.WriteLine();
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("          VECTOR STORE MENU");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("0. Exit form Store");
        Console.WriteLine("1. Add a new vector");
        Console.WriteLine("2. List Vectors");
        Console.WriteLine("3. Remove a vector");
        Console.WriteLine("4. Modify a vector");
        Console.WriteLine("5. Explore a Vector");
        Console.WriteLine("6. Redisplay Store Menu");
        //Farhan - someshit
        Console.WriteLine("7. Display Unit Vector");
        Console.WriteLine("8. Calculate Angle Between Two Vectors");

        Console.WriteLine();
    }

    // Method to Start Vector Store
    public void Init()
    {

        uint inputOption;
        DisplayVectorsStoreMenu();
        do
        {
            Console.WriteLine("6 to Redisplay Menu");
            Console.Write("Select an Option(Store): ");

            try
            {
                inputOption = uint.Parse(Console.ReadLine());
            }
            catch
            {
                inputOption = 999;
            }

            string name;
            switch (inputOption)
            {
                case 1:

                    Vector inputVector;
                    Console.WriteLine("\nInput a vector along with its name (e.g. v = 2i + 3j): ");
                    inputVector = Vector.Parse(Console.ReadLine());
                    if (inputVector != null) AddVector(inputVector);
                    else Console.WriteLine("Incorrect Vector Entered\n");

                    break;

                case 2:
                    ListVectors();
                    break;

                case 3:
                    ListVectors();
                    name = InputVectorName("Enter the vector name to be removed: ");
                    if (RemoveVector(name))
                        Console.WriteLine("Vector removed successfully\n");
                    break;

                case 4:
                    ListVectors();
                    name = InputVectorName("Enter the vector name to be modified: ");
                    if (EditVector(name))
                        Console.WriteLine("Vector modified successfully\n");

                    break;

                case 5:
                    ListVectors();
                    name = InputVectorName("Enter the vector name to be explored: ");
                    ExploreVector(name);
                    break;

                case 6:
                    DisplayVectorsStoreMenu();
                    break;

                case 7:
                    ListVectors();
                    name = InputVectorName("Enter the vector name to get the unit vector: ");
                    Vector vector = GetVector(name);
                    if (vector != null)
                    {
                        Vector unitVector = vector.GetUnitVector();
                        if (unitVector != null)
                        {
                            Console.WriteLine("Unit vector:");
                            DisplayVector(unitVector);
                        }
                    }
                    break;

                case 8:
                    ListVectors();
                    string name1 = InputVectorName("Enter the first vector name: ");
                    string name2 = InputVectorName("Enter the second vector name: ");
                    Vector v1 = GetVector(name1);
                    Vector v2 = GetVector(name2);
                    if (v1 != null && v2 != null)
                    {
                        double angle = Vector.GetAngleBetweenVectors(v1, v2);
                        if (!double.IsNaN(angle))
                            Console.WriteLine("Angle between {0} and {0}: {0:0.00} degrees\n", name1, name2, angle);
                    }
                    break;


                case 0:
                    Console.WriteLine("Exiting from store...\n");
                    break;



                default:
                    Console.WriteLine("Invalid Input\n");
                    break;
            }
        } while (inputOption != 0);

    }

    string InputVectorName(string message)
    {
        Console.Write(message);
        return Console.ReadLine().Trim().ToLower();
    }

    public Vector GetVector(string name)
    {
        int index = FindVector(name);
        if (index == -1)
        {
            return null;
        }

        return vectors[index];
    }

    bool AddVector(Vector v)
    {
        if (counter == STORE_SIZE)
        {
            Console.WriteLine("Store is full");
            return false;
        }

        if (FindVector(v.GetName()) != -1)
        {
            Console.WriteLine("Vector already exits (Vector name must be unique)\n");
            return false;
        }

        vectors[counter] = v;
        counter++;
        Console.WriteLine("Vector added successfully\n");
        return true;
    }

    int FindVector(string name)
    {
        int index = 0;
        for (uint i = 0; i < counter; i++)
        {
            if (vectors[i].GetName() == name)
                return index;

            index++;
        }

        return -1;
    }

    bool RemoveVector(string name)
    {
        int vectorIndex = FindVector(name);
        if (vectorIndex == -1)
        {
            Console.WriteLine("Vector with name {0} not found\n", name);
            return false;
        }

        for (int i = vectorIndex; i < counter - 1; i++)
            vectors[i] = vectors[i + 1];
        counter--;

        return true;
    }

    bool EditVector(string name)
    {
        int index = FindVector(name);
        if (index == -1)
        {
            Console.WriteLine("Vector with name {0} not found\n", name);
            return false;
        }

        DisplayVector(vectors[index]);

        Vector inputVector;
        Console.WriteLine("\nInput new vector along with its name (e.g. v = 2i + 3j): ");
        inputVector = Vector.Parse(Console.ReadLine());

        if (inputVector == null)
        {
            Console.WriteLine("Incorrect Vector Entered\n");
            return false;
        }

        vectors[index] = inputVector;

        return true;
    }

    public void DisplayVector(Vector vector)
    {
        if (vector == null) return;
        string xComponent = vector.GetXComponent() >= 0 ? vector.GetXComponent().ToString() : "- " + Math.Abs(vector.GetXComponent());
        string yComponent = vector.GetYComponent() >= 0 ? "+ " + vector.GetYComponent() : "- " + Math.Abs(vector.GetYComponent());
        string zComponent = vector.GetZComponent() >= 0 ? "+ " + vector.GetZComponent() : "- " + Math.Abs(vector.GetZComponent());

        Console.WriteLine(
          vector.GetName() + " = " +
          xComponent + "i " +
          yComponent + "j " +
          zComponent + "k"
        );

    }

    public void ListVectors()
    {
        Console.WriteLine("\n----------- Saved Vectors -----------\n");
        for (int i = 0; i < counter; i++) DisplayVector(vectors[i]);
        Console.WriteLine();
    }

    void ExploreVector(string name)
    {

        int vectorIndex = FindVector(name);
        if (vectorIndex == -1)
        {
            Console.WriteLine("Vector not found");
            return;
        }

        Console.WriteLine("\n---------- Vector Details ---------\n");
        DisplayVector(vectors[vectorIndex]);
        Console.WriteLine("Magnitude: {0:0.00}", vectors[vectorIndex].GetMagnitude());
        Console.WriteLine("X-Component: {0:0.00}", vectors[vectorIndex].GetXComponent());
        Console.WriteLine("Y-Component: {0:0.00} \n", vectors[vectorIndex].GetYComponent());
        Console.WriteLine("Z-Component: {0:0.00} \n", vectors[vectorIndex].GetZComponent());

    }
}


class VectorOperations
{
    public static Vector AddVectors(string name, Vector v1, Vector v2)
    {
        return new Vector(
            name,
            v1.GetXComponent() + v2.GetXComponent(),
            v1.GetYComponent() + v2.GetYComponent(),
            v1.GetZComponent() + v2.GetZComponent()
        );
    }

    public static Vector SubtractVectors(string name, Vector toVector, Vector fromVector)
    {
        return new Vector(
            name,
            fromVector.GetXComponent() - toVector.GetXComponent(),
            fromVector.GetYComponent() - toVector.GetYComponent(),
            fromVector.GetZComponent() - toVector.GetZComponent()
        );
    }

    public static Vector ScalarMultiplication(Vector v, double scalar)
    {
        return new Vector(
            "Result",
            scalar * v.GetXComponent(),
            scalar * v.GetYComponent(),
            scalar * v.GetZComponent()
        );
    }

    public static Double DotProduct(Vector v1, Vector v2)
    {
        return v1.GetXComponent() * v2.GetXComponent() +
               v1.GetYComponent() * v2.GetYComponent() +
               v1.GetZComponent() * v2.GetZComponent();
    }

    public static Vector CrossProduct(Vector v1, Vector v2)
    {
        double i = v1.GetYComponent() * v2.GetZComponent() -
                   v1.GetZComponent() * v2.GetYComponent();
        double j = v1.GetZComponent() * v2.GetXComponent() -
                   v1.GetXComponent() * v2.GetZComponent();
        double k = v1.GetXComponent() * v2.GetYComponent() -
                   v1.GetYComponent() * v2.GetXComponent();

        return new Vector("Result", i, j, k);
    }

    public static double Projection(Vector ofVector, Vector overVector)
    {
        return DotProduct(ofVector, overVector) / overVector.GetMagnitude();
    }

}

class VectorAnalyzer
{
    static void DisplayCommandsMenu()
    {
        Console.WriteLine();
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("                        MAIN MENU");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Vectors Store (To add, remove, modify, see vectors)");
        Console.WriteLine("2. Addition of Vectors");
        Console.WriteLine("3. Subtraction of Vectors");
        Console.WriteLine("4. Scalar Product");
        Console.WriteLine("5. Dot Product");
        Console.WriteLine("6. Cross Product");
        Console.WriteLine("7. Projection");
        Console.WriteLine("8. Redisplay Menu");
       

        Console.WriteLine();
    }

    static void Main()
    {

        Store store = new Store(20);
        uint inputOption;
        DisplayCommandsMenu();
        do
        {
            Console.WriteLine("8 to Redisplay Menu");
            Console.Write("Select an Option: ");

            try
            {
                inputOption = uint.Parse(Console.ReadLine());
            }
            catch
            {
                inputOption = 999;
            }

            Vector result;
            string v1Name, v2Name;
            double scalar, projection, dotProduct;
            switch (inputOption)
            {
                case 1:
                    Console.WriteLine("Welcome to Store");
                    store.Init();
                    break;

                case 2:
                    Console.WriteLine("Vector Addition");
                    store.ListVectors();
                    Console.Write("First Vector name: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("Second Vector name: ");
                    v2Name = Console.ReadLine().Replace(" ", "").ToLower();

                    try
                    {
                        result = VectorOperations.
                                            AddVectors(
                                            "Result",
                                            store.GetVector(v1Name),
                                            store.GetVector(v2Name)
                                            );
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input Vectors\n");
                        break;
                    }

                    store.DisplayVector(result);
                    Console.WriteLine();

                    break;

                case 3:
                    Console.WriteLine("Vector Subtraction");
                    store.ListVectors();
                    Console.Write("From Vector name: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("To Vector name: ");
                    v2Name = Console.ReadLine().Replace(" ", "").ToLower();

                    if (v1Name == null || v2Name == null) break;

                    try
                    {
                        result = VectorOperations.
                                            SubtractVectors(
                                            "Result",
                                            store.GetVector(v2Name),
                                            store.GetVector(v1Name)
                                            );
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input Vectors\n");
                        break;
                    }


                    store.DisplayVector(result);
                    Console.WriteLine();

                    break;

                case 4:
                    Console.WriteLine("Scalar Multiplication");
                    store.ListVectors();
                    Console.Write("Vector Name: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("Scalar Value: ");

                    try
                    {
                        scalar = Convert.ToDouble(Console.ReadLine());
                        result = VectorOperations.ScalarMultiplication(store.GetVector(v1Name), scalar);
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input\n");
                        break;
                    }

                    store.DisplayVector(result);
                    Console.WriteLine();

                    break;

                case 5:
                    Console.WriteLine("Dot Product");
                    store.ListVectors();
                    Console.Write("First Vector name: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("Second Vector name: ");
                    v2Name = Console.ReadLine().Replace(" ", "").ToLower();

                    try
                    {
                        dotProduct = VectorOperations
                                                .DotProduct(
                                                store.GetVector(v1Name),
                                                store.GetVector(v2Name)
                                                );
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input Vectors\n");
                        break;
                    }

                    Console.WriteLine("Dot Product: {0}\n", dotProduct);
                    break;

                case 6:
                    Console.WriteLine("Cross Product");

                    store.ListVectors();
                    Console.Write("First Vector name: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("Second Vector name: ");
                    v2Name = Console.ReadLine().Replace(" ", "").ToLower();
                    try
                    {
                        result = VectorOperations
                                        .CrossProduct(
                                                store.GetVector(v1Name),
                                                store.GetVector(v2Name)
                                                );
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input Vectors\n");
                        break;
                    }


                    store.DisplayVector(result);
                    Console.WriteLine();
                    break;

                case 7:
                    Console.WriteLine("Projection");
                    store.ListVectors();
                    Console.Write("Projection of vector: ");
                    v1Name = Console.ReadLine().Replace(" ", "").ToLower();

                    Console.Write("Projection over vector: ");
                    v2Name = Console.ReadLine().Replace(" ", "").ToLower();

                    try
                    {
                        projection = VectorOperations
                                                .Projection(
                                                store.GetVector(v1Name),
                                                store.GetVector(v2Name)
                                                );
                    }
                    catch
                    {
                        Console.WriteLine("Incorrect Input Vectors\n");
                        break;
                    }

                    Console.WriteLine("Projection of {0} over {1} is {2:0.00}\n", v1Name, v2Name, projection);
                    break;

                case 8:
                    DisplayCommandsMenu();
                    break;

                case 0:
                    Console.WriteLine("Exiting....");
                    break;

                default:
                    Console.WriteLine("Invalid Input\n");
                    break;
            }


        } while (inputOption != 0);

        // For Visual Studio 2012, Uncomment it
        // Console.ReadKey();
    }
}
