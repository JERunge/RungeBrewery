   M S B A M L   `   `   `  ����A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BrewUI.UserControlsBrewUI.UserControls  5      0    *BrewUI.UserControls.StepConfirmationWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:BrewUI.UserControls     5      $��10q�5      .��5      � -   5          �Name$  
LayoutRoot��5       � RowDefinitions 5	   
   �� 5
      $��10 �6   �� 5      $
��Auto �6   �� 5      $��10 �6   �� 5      $
��Auto �6   .+�5   
   �� $��1��69   ��6   �� *	ItemsList+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6!    K� IsItemsHost . 68   5      ��5      �� 5      .R�5      ��  �� Padding$ 5q�6      GrayDark#��[  6)   ._�5      � #��[  6    � ColumnDefinitions 5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63   $��50��6=   �� 5   "   $
��Auto �63   .+�5      �� $��0��6>   
�������$��0 0 10 0q�6a   ��6(   �� *Name+6a   �� 5       $��1��6A   
���������6(   �� *	Content+6Q   �� 5!      $��2��6E   
�������$��10 0 0 0q�6h    d� 	IsChecked 6'   �� *	Checked+6h   5"      5#      5$      5&      �� 5'   
   -   5         ��Name$
 Done��5'      $
��Done��62   $��3��6A   $
��Auto��6N   $��Center=�6[   5(      5)      