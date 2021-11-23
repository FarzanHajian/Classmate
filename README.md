# Classmate
A simple tool to conditionally add CSS classes to HTML elements in Razor components (and also Razor pages).

It is inspired by ReactJS [classnames](https://www.npmjs.com/package/classnames) library and brings such capabilities to Blazor applications.

## Installation
For now, there is no Nuget package (I promise to provide one soon). Just copy the Classmate.cs file from Classmate project into to your own project.

## Demo
Consider FetchData.razor component in the Blazor demo application. It generates an HTML table containg some weather forecasts uing the following code block:
```html
...
<table class="table">
    <thead>
        ...
    </thead>
    <tbody>
        @foreach (var forecast in forecasts)
        {
            <tr>
                <td>@forecast.Date.ToShortDateString()</td>
                <td>@forecast.TemperatureC</td>
                <td>@forecast.TemperatureF</td>
                <td>@forecast.Summary</td>
            </tr>
        }
    </tbody>
</table>
...
```

Now you want to use different styles (specially colors) for each row based on temperature values. 

First three CSS classes are needed.

```css
.cold {
    color: blue;
    font-style: italic;
    font-weight: normal;
}
.moderate {
    color: gray;
    font-style:normal;
    font-weight: normal;
}
.hot {
    color:red;
    font-style:normal;
    font-weight: bold;
}
```
And then the @foreach loop must be rewrited in either of the followng ways:

```
@foreach (var forecast in forecasts)
{
    int tp = forecast.TemperatureC;

    <tr class=@(
                Classes(
                    "cold".If(tp<=0),
                    "moderate".If(tp>0 && tp<40),
                    "hot".If(tp>=40)
                )
            )>
        <td>@forecast.Date.ToShortDateString()</td>
        <td>@forecast.TemperatureC</td>
        <td>@forecast.TemperatureF</td>
        <td>@forecast.Summary</td>
    </tr>
}
```
```
@foreach (var forecast in forecasts)
{
    int tp = forecast.TemperatureC;

    <tr class=@(
                Classes(new {
                    cold = tp<=0,
                    moderate= tp>0 && tp<40,
                    hot = tp>=40}
                )
            )>
        <td>@forecast.Date.ToShortDateString()</td>
        <td>@forecast.TemperatureC</td>
        <td>@forecast.TemperatureF</td>
        <td>@forecast.Summary</td>
    </tr>
}
```

## Usage
The first step is to add the following using directive to your razor file:
```c
@using static Classmate.Classmate;
```
Now you can use `Classmate` by calling `Classes` method.

```c#
// List of strings
Classes("Foo", "Bar", "", "Baz Mar", null, " Zab  Faz ", " ")   // "Foo Bar Baz Mar Zab  Faz"

// List of strings with boolean queries
Classes("Foo".If(true),"Bar".If(false));    // "Foo"

// List of strings with boolean queries and else values
Classes("Foo".If(true, "Food"),"Bar".If(false, "Baz"));    // "Foo Baz"

// List of strings with lambda queries
 Classes("Foo".If(() => true), "Bar".If(() => false));  // "Foo"

// List of strings with lambda queries and else values
 Classes("Foo".If(() => true, "Food"), "Bar".If(() => false, "Baz"));  // "Foo Baz"

// List of objects with boolean values
Classes(
    new { Foo = true },
    new { Bar = false, Baz = true, Gaz = true }
);  // "Foo Baz Gaz"

// List of objects with lambda values
Classes(
    new { Foo = If(() => true) },
    new { Bar = If(() => false), Baz = If(() => true) }
);  // "Foo Baz"

// A mixure
Classes(
    "Foo",
    "Faz",
    "Bar".If(() => false),
    "Gaz",
    new
    {
        Maz = true,
        Naz = If(() => true),
        Laz = If(() => false)
    },
    "Baz".If(true || false)
);  // "Foo Faz Gaz Maz Naz Baz"
```
Moreover, there are some APIs that let you define if/else rules.
```c#
// Strings with boolean predicate
Classes(true, "Yes", "No"); // "Yes"

// Objects with boolean predicate
Classes(
    true,
    new
    {
        Yes = true,
        No = If(() => false),
        YesYes = If(() => true)
    },
    new
    {
        NoWay = true,
        NoNo = If(() => false)
    }
);  // "Yes YesYes"

// Strings with lambda predicate
Classes(() => true, "Yes", "No");   // "Yes"

// Objects with lambda predicate
Classes(
    ()=> true,
    new
    {
        Yes = true,
        No = If(() => false),
        YesYes = If(() => true)
    },
    new
    {
        NoWay = true,
        NoNo = If(() => false)
    }
);  // "Yes YesYes"

``` 



