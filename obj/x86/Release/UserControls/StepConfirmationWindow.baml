   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BrewUI.UserControlsBrewUI.UserControls  0    *BrewUI.UserControls.StepConfirmationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:BrewUI.UserControls     $��10q�.��� -     �Name$  
LayoutRoot�� � RowDefinitions �� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��1������ *	ItemsList+���� .R���  �� Orientation$ Vertical=� K� IsItemsHost . ���� .R���  �� Padding$ 5q�   GrayDark#��[  ._�� #��[   � ColumnDefinitions �� $
��Auto ��� $��* �$��50���� $
��Auto �.+��� $��0��
�������$��0 0 10 0q����� *Name+�� $��1��
����������� *	Content+�� $��2��
�������$��10 0 0 0q� d� 	IsChecked �� *	Checked+�� -    ��Name$
 Done��$
��Done��$��3��$
��Auto��$��Center=�