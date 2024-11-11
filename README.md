# Classmate
Classmate is a simple tool to conditionally add CSS classes to HTML elements in Razor components (and also Razor pages). The library is heavily inspired by ReactJS [classnames](https://www.npmjs.com/package/classnames) and brings such capabilities to Blazor applications.

## Installation
The preferred method for installing the library is to copy the Classmate.cs file into the destination project, but for those who prefer installing using package, the Classmate package is available on [NuGet](https://www.nuget.org/packages/FarzanHajian.Classmate/).

## Demo
Consider FetchData.razor component in the Blazor demo application. It generates an HTML table contaning some weather forecasts using the following code block:
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
Now imagine we would like to use different styles for each row based on temperature values. To achieve the effect, first of all, three CSS classes are needed.
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
And then the @foreach loop must be rewritten in order to make use of Classmate. Two different ways of doing so are provided below:
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
                    moderate = tp>0 && tp<40,
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
Pay attention to how the class attribute of <tr> is set. For each row, only one of the style names is returned from the Classes method and assigned to the row HTML.

## Usage
The first step is to add the following using directive to your razor file:
```c
@using static FarzanHajian.Classmate.Classmate;
```
Now `Classmate` is accessible via calling `Classes` method. It accepts different inputs.
```c#
// List of strings
Classes("btn", "btn-primary", "", "active btn-lg", null, " col   dropdown ", " ")   // "btn btn-primary active btn-lg col   dropdown"

// List of strings with boolean queries
Classes("info".If(true),"error".If(false));    // "info"

// List of strings with boolean queries and else values
Classes("info".If(true, "error"),"bold".If(false, "italic"));    // "info italic"

// List of strings with lambda queries
Classes("info".If(() => true), "error".If(() => false));  // "info"

// List of strings with lambda queries and else values
Classes("info".If(() => true, "error"), "bold".If(() => false, "italic"));  // "info italic"

// List of objects with boolean values
Classes(
    new { info = true },
    new { bold = false, italic = true, underlined = true }
);  // "info italic underlined"

// List of objects with lambda values
Classes(
    new { info = If(() => true) },
    new { bold = If(() => false), italic = If(() => true) }
);  // "info italic"

// A mixure of different values
Classes(
    "btn",
    "btn_primary",
    "active".If(() => false),
    "italic",
    new
    {
        bold = true,
        italic = If(() => true),
        underlined = If(() => false)
    },
    "hidden".If(true || false)
);  // "btn btn-primary italic bold italic hidden"
```
Moreover, some APIs let you define if/else rules.
```c#
// Strings with boolean predicate - Either the first or the second string is chosen
Classes(true, "info", "error");         // "info"

// Strings with a lambda predicate - Either the first or the second string is chosen
Classes(() => true, "info", "error");   // "info"

// Objects with boolean predicate - Either the first or the second object is evaluated
Classes(
    true,
    new
    {
        info = true,
        bold = If(() => false),
        active = If(() => true)
    },
    new
    {
        error = true,
        disabled = If(() => false)
    }
);  // "info active"

// Objects with a lambda predicate - Either the first or the second object is evaluated
Classes(
    () => true,
    new
    {
        info = true,
        bold = If(() => false),
        active = If(() => true)
    },
    new
    {
        error = true,
        disabled = If(() => false)
    }
);  // "info active"
``` 