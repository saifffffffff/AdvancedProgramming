
namespace Lessons;

sealed class stringBuilder
{

    // fields

    int _length;

    int _capacity = 16;

    char[] _arr;

    stringBuilder? _previous;

    // constructors

    public stringBuilder(string? value)
    {
        ArgumentNullException.ThrowIfNull(value, "value");

        _capacity = value.Length > _capacity ? value.Length : _capacity; // default is 16

        _arr = new char[_capacity];

        _length = value.Length;

        value.CopyTo(0, _arr, 0, value.Length);


    }

    // copy constructor 
    public stringBuilder(stringBuilder from)
    {
        _capacity = from._capacity;
        _length = from._length;
        _arr = from._arr;
        _previous = from._previous;
    }

    // methods

    public void Append(string? valueToAppend)
    {

        ArgumentNullException.ThrowIfNull(valueToAppend, "value");

        int remainingSpace = _capacity - _length;


        if (valueToAppend.Length <= remainingSpace)
        {
            valueToAppend.CopyTo(0, _arr, _length, remainingSpace);
            _length += valueToAppend.Length;
        }

        else
        {
            // fill the remaining empty slots
            if (remainingSpace > 0)
            {
                valueToAppend.CopyTo(0, _arr, _length, remainingSpace);
                _length = _arr.Length;
            }
            int remainingCharacters = valueToAppend.Length - remainingSpace;

            CreateNewChunk(remainingCharacters);

            valueToAppend.CopyTo(valueToAppend.Length - remainingCharacters, _arr, 0, remainingCharacters);


        }


    }

    private void CreateNewChunk(int remainingCharacters)
    {

        // chaning the object fields 

        _previous = new stringBuilder(this);
        _length = 0;
        _capacity = Math.Max(remainingCharacters, _capacity);
        _arr = new char[_capacity];


    }


    // properities 

    public int Length
    {
        get
        {
            int sumLength = 0;

            for (stringBuilder current = this; current != null; current = current._previous)
                sumLength += current._length;

            return sumLength;
        }
    }
    public int Capacity
    {
        get
        {
            int sumCapacity = 0;

            for (stringBuilder current = this; current != null; current = current._previous)
                sumCapacity += current._capacity;

            return sumCapacity;
        }
    }

    private void PrintArr(char[] arr)
    {
        foreach (char c in arr)
            Console.Write(c);

        Console.WriteLine();
    }

    public void Print()
    {

        for (stringBuilder temp = this; temp != null; temp = temp._previous)
            PrintArr(temp._arr);
    }

    public override string ToString()
    {
        return "will be implemented soon";
    }



}