   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BrewUI.ViewsBrewUI.Views       BrewUI.Views.AddGrainsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:BrewUI.Views       � Title$   Strike temperature reached��$	��300��$	��400�� � WindowStartupLocation$ CenterOwner=� � WindowStyle$ 
ToolWindow=�.��� $��10q� � ColumnDefinitions �� $��* ��� $
��Auto � � RowDefinitions �� $��20 ��� $��10 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $<��6Please add grains and press Continue to start mashing.���� $��2��$��2������ *	GrainList+���� .R���  �� Orientation$ Vertical=� K� IsItemsHost . ���� .R��    GrayDark#��[  $��0 2 0 0q� �� $��5 ��� $
��Auto ��� $��* ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��5 � �� $��30 �.+��� $��1��$��10 0 0 0q�
�������$��Center=����� *name+�� $��3��$��40�� �� TextAlignment$ Right=�
�������$��10 0 0 0q�$��Center=����� *amount+ �� StringFormat$
 {0}g���� $��5��$��Center=�	 d� 	IsChecked	 �� *added+�� -   
 ��Name$
 Continue��$��Continue��$��2��$��4��{��� *GrainsAdded+