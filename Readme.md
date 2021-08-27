<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128657413/14.1.10%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T226937)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DragDropResourcesHelper.cs](./CS/DragDropResourcesHelper.cs) (VB: [DragDropResourcesHelper.vb](./VB/DragDropResourcesHelper.vb))
* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
<!-- default file list end -->
# How to reorder resources in SchedulerControl views via the "drag-drop" operation (WPF version)


<p>The main idea of this approach is to interchange a value of a custom field between the "dragged" and "dropped" resources.<br />An approach to sort (reorder) resources based on a custom field value was demonstrated in the following example:<br /><a href="https://www.devexpress.com/Support/Center/p/E3124">How to sort resources</a><br /><br />To build a solution demonstrated in this sample into an existing application, copy the "<strong>DragDropResourcesHelper</strong>" module and pass a current SchedulerControl instance and the "sort" field name into the <strong>DragDropResourcesHelper </strong>class constructor:<br /><br /></p>


```cs
DragDropResourcesHelper helper = new DragDropResourcesHelper(schedulerControl1, "SortOrder");
```




```vb
Dim helper As New DragDropResourcesHelper(schedulerControl1, "SortOrder")
```



<br/>


