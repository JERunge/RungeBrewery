   M S B A M L   `   `   `  ����A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BrewUI.ViewsBrewUI.Views  5   	        BrewUI.Views.AddGrainsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BrewUI.Views     5   	     � Title$   Strike temperature reached��5   	   $	��300��6,   $	��400��69    � WindowStartupLocation$ CenterOwner=�5	   	    � WindowStyle$ 
ToolWindow=�6-   .��5
      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��10 �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $<��6Please add grains and press Continue to start mashing.��6   �� 5   
   $��2��69   $��2��6F   ��6   �� *	GrainList+��5      �� 5      .R�5      ��  �� Orientation$ Vertical=�6!    K� IsItemsHost . 68   5      ��5      �� 5      .R�5      �    GrayDark#��[  6   $��0 2 0 0q�6B    5       �� 5!      $��5 �6/   �� 5"      $
��Auto �6/   �� 5#      $��* �6/   �� 5$      $
��Auto �6/   �� 5%      $��10 �6/   �� 5&      $
��Auto �6/   �� 5'      $��5 �6/    5)      �� 5*      $��30 �6,   .+�5,      �� $��1��6:   $��10 0 0 0q�5-   8   
�������$��Center=�6]   ��5,   $   �� *name+5-   ]   �� 5.      $��3��6Q   $��40��6a    �� TextAlignment$ Right=�6l   
�������$��10 0 0 0q�5/   8   $��Center=�6J   ��5.   $   �� *amount+ �� StringFormat$
 {0}g��5/   J   �� 50      $��5��6?   $��Center=�6O   	 d� 	IsChecked	 6#   �� *added+6O   51      52      54      �� 55   
   -   5        
 ��Name$
 Continue��55      $��Continue��6#   $��2��56      $��4��6!   {�57      �� *GrainsAdded+59      5:      