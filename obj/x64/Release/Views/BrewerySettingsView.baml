   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BrewUI.PropertiesBrewUI.Properties  +clr-namespace:BrewUI.ViewsBrewUI.Views  &     BrewUI.Views.BrewerySettingsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -pclr-namespace:BrewUI.Properties     ,localclr-namespace:BrewUI.Views       � Title$  Settings��$	��930��$
��1000�� � WindowStartupLocation$ CenterOwner=� � WindowStyle$ 
ToolWindow=� � 	Resources  �� 
TargetType   MainContainer �� Property �� Value  	InputText  	UnitBlock  AccentColor*  $System.Windows.Controls.DataGridCell��%�  &	        &	 �     &	     '�� �    7  '  ~    �� " ��.���  � ��
 �������  � ��$ 1q���  �@��Padding$ 10q���  �@��CornerRadius$ 3����  � ��$ 0 0 0 20q��� " ��.���  � ��$ 50����  � ��$ Right=���  � ��$ 20����  � ��$ 0 0 0 5q��� " ��.���  � ��
 �������  � ��$ 30����  � ��$ 20����  �@��Padding$ 0 3 0 0q���  � ��$ 0 0 0 5q���  � ��$ 10(���  � ��$ Right=���  �@��TextAlignment$ Center=��� " ��.���  � ��8   ��  � ��$ 10 0 0 0q���  � ��$ 50����  � ��
 �������  � ��$ 0q���  � ��$	 0 5q��� "  .���  � ��$
 Wrap=�.���  � ColumnDefinitions �� $��20 ��� $��* ��� $��20 ��� $
��Auto ��� $��20 � � RowDefinitions �� $��20 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��20 �.+��� $��3��$��1��#��[  ._��� 	 �� Orientation$	 Vertical=�.+��� $��	Equipment��  Header1#��[ �  �� $
��Auto ��� $��* ��� $
��Auto � �� $��10 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��
Batch size��$��0��$��1��]� $��2��$��1��.+��� #��[ ���� 
 �� Path$
 	BatchSize� �� Mode$ TwoWay=� �� Delay$	 250���� $��L��#��[ �� $��Heater effect��$��0��$��2��]� $��2��$��2��.+��� -    ��Name$ HeaterEffect��#��[ �� $��kW��#��[ �� $��Fermenter loss��$��0��$��3��]� $��2��$��3��.+��� -   $ FermenterLoss��#��[ �� $��L��#��[ �� $��Kettle loss��$��0��$��5��]� $��2��$��5��.+��� -   $ 
KettleLoss��#��[ �� $��L��#��[ �� $��3��$��2��#��[  ._��� $	 Vertical=�.+��� $��General��#��[ �  �� $	��120 ��� $��* ��� $
��Auto � �� $��10 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Mash thickness��$��0��$��2��]� $��2��$��2��.+��� -   $ MashThickness��#��[ �� $
��L/kg��#��[ �� $��Grain absorption��$��0��$��3��]� $��2��$��3��.+��� -   $ GrainAbsorption��#��[ �� $
��L/kg��#��[ �� $��Cooling shrinkage��$��0��$��4��]� $��2��$��4��.+��� -   $ CoolingShrinkage��#��[ �� $��%��#��[ �� $��Evaporation rate��$��0��$��5��]� $��2��$��5��.+��� -   $ EvaporationRate��#��[ �� $��%��#��[ �� $��Pump on��$��0��$��8��]� $��2��$��8��.+��� -   $ PumpOnDuration��#��[ �� $��s��#��[ �� $��3��$��6��#��[  $	��Top=�._��� $	 Vertical=�.+��� $��
Connection��#��[ $��0 0 0 10q�	� -	    	�Name$ BluetoothEnabled��$��	Bluetooth�� 	� 	GroupName$ 
Connection��	� -
   $ WifiEnabled��$
��Wifi��$ 
Connection��� $��0 10 0 0q� �� $
��Auto ��� $��5 ��� $��* �.+��� $��Brewery IP:��$��0���� -   $ 	breweryIP��$��2��� $��7��$��3�� �� $��* ��� $
��Auto �.+��� $	 
Horizontal=�$��1��.+��� -    ��Name$ CancelSettings��$��Cancel���� -   $ ResetSettings��$��Reset���� -   $ ApplySettings��$��Apply���� $��1��$��1��$��2��#��[  $
��Auto��._��� $	 Vertical=�.+��� $��Ingredient inventory��#��[ $��0 0 0 20q��� -   $ ImportIngredients��$��Import indredients��$
��Left=�$	��120��$��0 0 0 20q��� $
��Auto��$	��310��.@��� $
��Hops��.��&   System.Windows.Controls.DataGrid  -     Name$ HopList��$
��Auto��   MaxColumnWidth$	 200Q�   CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly .   	GrayLight   AlternatingRowBackground# [    AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name+  $ Origin�� �� *Origin+  $ Alpha�� �� *Alpha+  $
 Beta�� �� *Beta+  $ Notes�� �� *Notes+�� $��Grains��.��  -   $ 	GrainList��$
��Auto��$	 200Q� .   .   . # [  .     $
 Name�� �� *name+  $ Origin�� �� *origin+  $ Notes�� �� *notes+�� $��Yeast��.��  -   $ 	YeastList��$
��Auto��$	 200Q� .   .   . # [  .     $
 Name�� �� *name+  $	 Lab�� �� *origin+  $ Best for�� �� *	bestFor+  $ Notes�� �� *notes+��   ContentBorder#��[ $��3��$��1��$��5��._��� $	 Vertical=�.+��� $��Equipment inventory��#��[ �� -   $ ImportEquipment��$��Import equipment��$	��120��$
��Left=�$��	0 20 0 20q��� $	��310��.@��� $��Styles��.��  -   $ 	StyleList��$
��Auto��$	 200Q� .   .   . # [ 