   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BrewUI.UserControlsBrewUI.UserControls  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf     BrewUI.Views.SessionView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0uc!clr-namespace:BrewUI.UserControls     H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ���#2d3436$
��Auto��$
��Auto��$��Stretch=�$	��580��  � 	Resources   �� 
TargetType �� Property �� Value��%1   '��       �� " ��.���  � ��$ Center=�.���  � ColumnDefinitions �� $	��150 ��� $��20 ��� $��* ��� $��20 ��� $	��150 � � RowDefinitions �� $
��Auto ��� $��20 ��� $��* ��� $��20 ��� $��* �$	��200���� $��50 �.+��� $��0��$��0��   ContentBorder#��[  ._���  �� Orientation$ Vertical=�.+��� -    ��Name$ SessionName��$��16(�$��0 0 0 15q�$
��Bold"�$
��Wrap=��� $ 
Horizontal=�.+��� ����  �� Path$ 	BatchSize�	 �� StringFormat$	 {0} L���� $ 
Horizontal=�.+��� -   $ 
BrewMethod��$��Right=��� $ 
Horizontal=�.+��� -   $ 	BeerStyle���� $��0��$��2��#��[  ._��  �� $
��Auto ��� $��20 ��� $
��Auto � �� $��0 ��� $��* �$	��100���� $��20 ��� $��* �$	��100���� $��0 �.+��� $ Vertical=�$��1��$��0��.+��  �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��40(�
 �� TextAlignment$
 Center=�$��1������ *CurrentTemp+�� $	��°C��$��18(�$��2��$��0 0 0 10q��� $��CURRENT��$
 Center=�
��������� $ Vertical=�$��3��$��0��.+��  �� $��* ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��40(�$
 Center=�$��1������ *
TargetTemp+�� $	��°C��$��18(�$��2��$��0 0 0 10q��� $��TARGET��$
 Center=�
��������� $ Vertical=�$��1��$��0��.+��� -   $ MainTimerText��$��50��$

 Left=�$
��Left=�-  'MahApps.Metro.Controls.MetroProgressBar  $��2��$��0��$��5��$��0 5 0 0q� � Maximum$	 100Q� � Minimum$ 0Q�
�������$��10��$y�Visible=�  AccentColor#��[  � Value �� *SessionProgress+�� $ Vertical=�$��1��$��4��.+��� -   $ TimeLeftText��$
 Right=�$��Right=��� $��5��$��0��#��[  ._��� $ Vertical=�.+��� $��Status��  Header1#��[ $
��Bold"�$��0 0 0 10q���   Header2#��[ ���� *CurrentProcess+�� #��[ ���� *CurrentStep+�� ���� *CurrentWork+� $��0��$��2��$��5�� �� $��* � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� -    ��Name$ MashContainer��$��0��#��[  ._�9� -    9�Name$ MashExpander�� 9� 
IsExpanded . ���� $
��Mash��#��[ .���� $ Vertical=�$��0 10 0 0q�.+��� $��0 10 0 0q����� *	MashSteps+���� .R��� $ Vertical=� K� IsItemsHost . ���� .R�� ���� " � �� Triggers ��  �� Binding �� *Status+ �� Value Finished.a���  � ��  	GrayLight# [ ��  �� *Status+ Waiting.a���  � ��  GrayDark# [ ��  �� *Status+ Mashing.a���  � ��# [ ��  �� *Status+
 Paused.a���  � ��# [ ��  �� *Status+ 
Preheating.a���  � ��# [  �� $��10 ��� $	��200 ��� $��* ��� $	��100 ��� $��* ��� $	��100 ��� $��* ��� $��50 ��� $��5 � �� $��30 �.+��� $��1��$��10 0 0 0q�
�������$��Center=����� *
stepName+�� $��3��$��10 0 0 0q�
�������$��Center=����� *Status+�� $��5��$��10 0 0 0q�
�������$��Center=����� *	TimerText+�� $��7��$��10 0 0 0q�
�������$��Center=����� *
stepTemp+$	 {0}ºC��� $��0 0 0 0q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� -    ��Name$ 	StartMash��$��2��$��Center=�$��90��$��0 10 0 0q����� " �� �� BasedOn# [B�� ��  �� *MashRunning+ True.a���  � ��$ Cancel����  �� *MashRunning+	 False.a���  � ��$ Start����  �� *MashFinished+ True.a���  � {� .  ��  �� *MashFinished+	 False.a���  � {� . �� -	   $ SpargeContainer��$��2��#��[  ._�9� -
   $ SpargeExpander������ $��Sparge��#��[ .��� $��0 10 0 0q� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� -   $ SpargeTimerText��$��0���� $ 
Horizontal=�$��2��$��0��.+��� $��0 0 30 0q����� $ 
SpargeTemp�$!	 Target temperature: {0} °C���� ���� $ 	SpargeDur�$	 Duration: {0} min���� $ 
Horizontal=�$��2��$��2��.+��� -   $ 
TogglePump��$��90��$��0 0 10 0q����� " ��# [B�� ��  �� *CanTogglePump+ True.a���  � {� . ��  �� *CanTogglePump+	 False.a���  � {� .  ��  �� *PumpOn+ True.a���  � ��$ Pump off����  �� *PumpOn+	 False.a���  � ��$ Pump on���� -   $ StartSparge��$��2��$��2��$��90������ " ��# [B�� ��  �� *SpargeRunning+ True.a���  � ��$ Cancel����  �� *SpargeRunning+	 False.a���  � ��$ Start����  �� *SpargeFinished+ True.a���  � {� .  ��  �� *SpargeFinished+	 False.a���  � {� . �� -   $ BoilContainer��$��4��#��[  ._�9� ���� $
��Boil��#��[ .���� $ Vertical=�.+��� $ 
Horizontal=�.+��� $��0 0 30 0q����� $ 
BoilStatus�$	 Status: {0}���� ���� $ BoilTimerText�$	 Time left: {0}���� $��0 10 0 0q����� *	BoilSteps+���� .R��� $ Vertical=� . ���� .R���  �� Padding$ 5q�$��0 10 0 0q�$��0q� �� CornerRadius$ 3������ " �� ��  �� *added+	 False.a���  � ��# [ ��  �� *added+ True.a���  � ��# [ ._��  �� $��* � �� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 �.+��� $��1��$��1��#��[ 
�������$��Center=�$��Center=����� *
boilTime+�� $��1��$��3��$��3������ *
hopsList+���� .R��� $ Vertical=� . ���� .R��� $ 
Horizontal=�$��Center=�$��Center=�.+��� $	��200��
�������$

 Left=����� *Name+�� $��50��
�������$
 Right=����� *Amount+$	 {0} g���� -   $ 	StartBoil��$��Right=�$��0 10 0 0q�$��90������ " ��# [B�� ��  �� *BoilRunning+ True.a���  � ��$ Cancel����  �� *BoilRunning+	 False.a���  � ��$ Start����  �� *BoilFinished+ True.a���  � {� .  ��  �� *BoilFinished+	 False.a���  � {� . �� -   $ CooldownContainer��$��6������ " ��# [   ��  �� *RunCooldown+ True.a���  � y�$ Visible=���  �� *RunCooldown+	 False.a���  � y�$ 	Collapsed=�._�9� ���� $��Cooldown��#��[ .���� $ Vertical=�.+�� .+��� $ 
Horizontal=�$��3��.+��� $#��Cooldown target temperature: ���� -   $ CDTargetTemp���� -   $ StartCooldown��$��Start��$��90��$��Right=�$��2��$��2������ " ��# [B�� ��  �� *CooldownRunning+ True.a���  � ��$ Cancel����  �� *CooldownRunning+	 False.a���  � ��$ Start����  �� *CooldownFinished+ True.a���  � {� .  ��  �� *CooldownFinished+	 False.a���  � {� . #  LiveCharts.Wpf.CartesianChart  $��4��$��0��$��5��&   LiveCharts.Wpf.Charts.Base.Chart   	Hoverable . $
��Auto��$
��Auto��$	��200��   DataTooltip Z�    Series   LiveCharts.Wpf.LineSeries     LineSmoothness$ 0Q�   PointGeometrySize$ 0Q�  LiveCharts.Wpf.Series   Values �� *chartValues+   AxisX   LiveCharts.Wpf.Axis      Title$
  Time��!   LabelFormatter! �� *DateTimeFormatter+"   Unit" �� *
AxisUnit+#   AxisY#   $  Temperature��$   MinValue$$ 0Q�%   MaxValue$	% 105Q��� -   $ 
BackButton��$��0��$��7��$��70��$
��Left=����� $ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]&   Kind$& NavigateBefore=�$��-4 1 0 0q�$��Bottom=��� $
��Back��