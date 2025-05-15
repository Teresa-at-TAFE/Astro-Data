//Teresa McCutcheon, Newbies, Sprint Number 1
//Date: 27 / 03 / 2025
//Version: 1.0
//Name of the program: Astronomical Processing 
//Brief explanation of the program and list,
//Inputs, Processes, Outputs: 
//Program accepts data from observatory, and provides
//user access to search and manipulate said data. -->

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Astronomical_Processing;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    //This is the event handler for the "Populate" button click
    //This method populates the "Neutrino Interactions" text box with a simulated download of data
    private void Populate_Click(object sender, RoutedEventArgs e)
    {
        var rand = new Random();

        // Create an array to hold 24 random numbers.
        // These numbers will represent the simulated data.
        int[] numbers = new int[24];

        // Loop through each element in the numbers array to generate a random value.
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rand.Next(10, 91); // Generates random numbers from 10 to 90
        }

        // Convert the array of numbers into a single string, where each number is separated by a new line.
        // Fills the TextBox with values, one per line
        RandomValues.Text = string.Join(Environment.NewLine, numbers);
    }

    // This is the event handler for the "Sort" button click
    // This method performs a bubble sort on the list of numbers displayed in the RandomValues TextBox.
    private void Sort(object sender, RoutedEventArgs e)
    {
        // Split the contents of the TextBox into individual lines (each line should contain one number)
        string[] lines = RandomValues.Text.Split([Environment.NewLine], StringSplitOptions.None);
        // Create an array to store the parsed integer values
        int[] numbers = new int[lines.Length];

        // Convert each line from the TextBox into an integer and store it in the array
        for (int i = 0; i < lines.Length; i++)
        {
            numbers[i] = int.Parse(lines[i]);
        }

        // Perform a bubble sort on the array of numbers
        // This nested loop repeatedly steps through the list, compares adjacent elements,
        // and swaps them if they are in the wrong order (ascending sort)
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            for (int j = 0; j < numbers.Length - i - 1; j++)
            {
                // Compares the current number with the next one
                if (numbers[j] > numbers[j + 1])
                {
                    // Swaps the two numbers if they are in the wrong order
                    var temp = numbers[j];
                    numbers[j] = numbers[j + 1];
                    numbers[j + 1] = temp;
                }
            }
        }

        // Updates the TextBox to display the sorted numbers, one per line
        RandomValues.Text = string.Join(Environment.NewLine, numbers);
    }

    //Binary search method
    private int BinarySearchMethod(int[] numbers, int target)
    {
        int low = 0;
        int high = numbers.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (numbers[mid] == target)
            {
                return mid;
            }
            else if (numbers[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }

        }//end while

        return -1;
    }//end BinarySearchMethod

    //Definition for Search button click
    private void Search(object sender, RoutedEventArgs e)
    {
        // Attempt to parse the user's input from the search field
        if (!int.TryParse(Search_Field.Text, out int target))
        {
            //Feedback to user if incorrect type of search parameter entered
            MessageBox.Show("Please enter a valid number to search for.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Convert the lines from the TextBox into an array of integers
        int[] numbers = RandomValues.Text
            .Split(new[] { Environment.NewLine }, StringSplitOptions.None)  
            .Select(line => int.Parse(line))      // Convert each line to an integer
            .ToArray();                          // Convert the result to an array                                                           // Convert to an array

        // Perform binary search
        int resultIndex = BinarySearchMethod(numbers, target);

        // Display the result
        string message = resultIndex != -1
            ? $"Number {target} found at index {resultIndex}."
            : $"Number {target} not found in the list.";

        MessageBox.Show(message, "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Edit(object sender, RoutedEventArgs e)
    {
        // Get the value to find
        if (!int.TryParse(Select.Text, out int valueToFind))
        {
            MessageBox.Show("Please enter a valid number to find.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Get the new value to replace it with
        if (!int.TryParse(Edit_Field.Text, out int newValue))
        {
            MessageBox.Show("Please enter a valid new number.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Split the current values into a list
        string[] lines = RandomValues.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        bool found = false;

        // Replace all matching values
        for (int i = 0; i < lines.Length; i++)
        {
            if (int.TryParse(lines[i], out int currentVal) && currentVal == valueToFind)
            {
                lines[i] = newValue.ToString();
                found = true;
            }
        }

        if (!found)
        {
            MessageBox.Show($"Value {valueToFind} is not available, please try again.", "Not Found", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        // Update the text box
        RandomValues.Text = string.Join(Environment.NewLine, lines);
    }
}
