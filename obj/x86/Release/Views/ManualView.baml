   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  
 BrewUI   BrewUI.Views.ManualView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     .���   � RowDefinitions  �� $��20 ��� $
��auto � � ColumnDefinitions �� $	��120 ��� $��20 ��� $
��auto �.+���  �� Orientation$ Vertical=�$��1��.+��� -    ��Name$ 
TogglePump��$��Pump On/Off��$��25���� -   $ ToggleHeater��$��Heater On/Off��$��25���� -   $ GetTemp��$��Get temperature��$��25���� -   $ GetInfo��$��Get information��$��25���� $��Temperature:��$��0 10 0 0q��� -    ��Name$ 
TargetTemp��$��25���� $��	Duration:��$��0 10 0 0q��� -   $ TargetDuration��$��25���� -   $ HeatAndKeep��$��Heat and keep��$��25���� -    ��Name$ CurrentAction��$��	0 30 0 30q� �� TextAlignment$ Center=��� -	   $ 
CountValue���� -
   $ MessageToWifi��$	��200���� -   $ 
SendToWifi��$��Send to wifi��$��25���� -   $ StartClient��$��Start client��$��25���� -   $ StartServer��$��Start server��$��25��#   LiveCharts.Wpf.CartesianChart  $��1��$��2��&    LiveCharts.Wpf.Charts.Base.Chart   	Hoverable .  $	��500��   DataTooltip Z� 	   Series	    LiveCharts.Wpf.LineSeries  
   LineSmoothness$
 0Q�   PointGeometrySize$ 0Q�   LiveCharts.Wpf.Series   Values �� *chartValues+   AxisX    LiveCharts.Wpf.Axis     Title$ Elapsed time��   LabelFormatter �� *DateTimeFormatter+   Unit �� *
AxisUnit+   MaxValue �� *
XAxisMax+   MinValue �� *
XAxisMin+   	Separator    LiveCharts.Wpf.Separator     Step �� *
AxisStep+   AxisY   $ Temperature��