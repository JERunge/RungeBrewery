   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      
 BrewUI   BrewUI.Views.ManualView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      .��5	      �   � RowDefinitions  5
   
   �� 5      $��20 �6   �� 5      $
��auto �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   �� 5      $��20 �6   �� 5      $
��auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��1��6,   .+�5      �� -   5         ��Name$ 
TogglePump��5      $��Pump On/Off��6)   $��25��6?   �� 5      -   5        $ ToggleHeater��5      $��Heater On/Off��6+   $��25��6C   �� 5      -   5        $ GetTemp��5      $��Get temperature��6&   $��25��6@   �� 5      -   5        $ GetInfo��5      $��Get information��6&   $��25��6@   �� 5      $��Temperature:��6   $��0 10 0 0q�6,   �� 5      -   5         ��Name$ 
TargetTemp��5      $��25��6*   �� 5      $��	Duration:��6   $��0 10 0 0q�6)   �� 5      -   5        $ TargetDuration��5      $��25��6.   �� 5      -   5        $ HeatAndKeep��5      $��Heat and keep��6*   $��25��6B   �� 5      -   5         ��Name$ CurrentAction��5      $��	0 30 0 30q�6/    �� TextAlignment$ Center=�6B   �� 5!      -	   5        $ 
CountValue��5!      �� 5"      -
   5        $ MessageToWifi��5"      $	��200��6-   �� 5#      -   5        $ 
SendToWifi��5#      $��Send to wifi��6)   $��25��6@   �� 5$      -   5        $ StartClient��5$      $��Start client��6*   $��25��6A   �� 5%      -   5        $ StartServer��5%      $��Start server��6*   $��25��6A   5&      #   LiveCharts.Wpf.CartesianChart  5)   
   $��1��6   $��2��6*   &    LiveCharts.Wpf.Charts.Base.Chart   	Hoverable .  $	��500��5+         DataTooltip 5*   /   Z� 	   Series	 5.         LiveCharts.Wpf.LineSeries  
   LineSmoothness$
 0Q�5/   !      PointGeometrySize$ 0Q�50   !      LiveCharts.Wpf.Series   Values 5.   !   �� *chartValues+50   !      AxisX 53         LiveCharts.Wpf.Axis     Title$ Elapsed time��54         LabelFormatter 53      �� *DateTimeFormatter+   Unit 55      �� *
AxisUnit+   MaxValue 56      �� *
XAxisMax+   MinValue 57      �� *
XAxisMin+   	Separator 58         LiveCharts.Wpf.Separator  59         Step 6(   �� *
AxisStep+5;         AxisY 5>        $ Temperature��6   5@      5B      5C      