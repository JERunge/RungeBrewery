   M S B A M L   `   `   `  ����A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BrewUI.UserControlsBrewUI.UserControls  5	      &     BrewUI.Views.SessionSettingsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Aa9http://schemas.microsoft.com/expression/2010/interactions  5      *uc!clr-namespace:BrewUI.UserControls  5	      $
��Auto��5      $��Stretch=�6     � 	Resources  5       �� 
TargetType �� Property �� Value��%s  '��       '�� p     �� 5   
   " ��6   .�5      ��  � ��$ 18��6'   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  �@��Padding$ 10q�6(   �� 5       � ��
 �����6+   �� 5       � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       �@��CornerRadius$ 3��6-   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5!      $
��Auto �6   �� 5"      $��20 �6   �� 5$      $
��Auto �6   �� 5%      $��20 �6   �� 5'      $
��Auto �6   �� 5(      $��20 �6   �� 5*      $
��Auto �6   �� 5+      $��20 �6   �� 5-      $
��Auto �6   �� 5.      $��20 �6   �� 50      $��* �6   .+�54   
   �� $��0��6   $��Stretch=�6   ._�55      ��  �� Orientation$ Vertical=�6     56      ��%X   '��       �� 57      " ��6   .�58      ��  � ��$ 0 0 0 10q�63   59      5:      .+�5;      � $��0 0 15 0q�6    5<      �� 5=      $
��Auto �6+   �� 5>      $��* �6+   �� 5?      $
��Auto �6+   .+�5A      �� $��
Properties��6       Header1#��[  62   $��0��5B       $��0 0 0 15q�5C       5D      � 5F       � Orientation$ 
Horizontal=�6   .+�5G      �� $��0 0 10 0q�6   ._�5H      �� $ Vertical=�6%   .+�5I      �� $
��Name��6(   �� 5J      -   5         ��Name$ SessionName��5J   &   $	��220��6;   $��20��6G   $��Italic$�5K   &   .��6y   ��5L   "   �� 5M   &   	 �� Triggers	 5N   *   �� 5O   .   
 �� Binding
 6:   �� *SessionNameBorder+ �� Value True6`   .a�5P   2   ��  � ��$ 1q�6\   5Q   /   �� 5R   .   
 6:   �� *SessionNameBorder+	 False6`   .a�5S   2   ��  � ��$ 0q�6\   5T   /   5V   '   .��5W   1   5X      5Y      5Z      �� 5\      $��0 0 10 0q�6   ._�5]      �� $ Vertical=�6%   .+�5^      �� $��
Batch size��6(   �� 5_      $ 
Horizontal=�6)   .+�5`   "   �� -   5        $ 	BatchSize��5`   *   $��30��6=   $��18��6H   �� 5a   "   $�� liter��6,     	UnitBlock#��[ 6:   5b      5c      5d      �� 5f      $��0 0 10 0q�6   ._�5g      �� $ Vertical=�6%   .+�5h      �� $��Method��6(   �� 5i      $	��100��5k      ��5i   '   �� *BrewMethods+ �� SelectedItem 5j      ��  �� Path$ SelectedBrewMethod� �� Mode$ TwoWay=���5l   "   �� 5m   &   .R�5n   *   �� ��64   �� *Name+5o   '   5q      5r      5s      �� 5u      ._�5v      �� $ Vertical=�6%   .+�5w      �� $��Style��6(   �� 5x      $	��150��5z      ��5x   '   �� *	StyleList+ 5y      �� $ SelectedBeerStyle�$ TwoWay=���5{   "   �� 5|   &   .R�5}   *   �� ��64   �� *Name+5~   '   5�      5�      5�      5�      5�      5�      �� 5�   
   -   5         ��Name$ MashContainer��5�   .   $��2��6   $��0��6   ._�5�      9�  9� 
IsExpanded . ��5�      �� 5�      $
��Mash��6    #��[  6,   .��5�      �  5�      �� 5�      $��* �6+    5�      �� 5�      $
��Auto �6(   �� 5�      $��20 �6(   �� 5�      $
��Auto �6(   �� 5�      $��20 �6(   �� 5�      $
��Auto �6(   .+�5�      �� $ Vertical=�6!   $��0��68   $��2��6H   .+�5�      �� $��
Add grains��6$     Header2#��[ 66   $��0 0 0 10q�6W   � 5�       5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��5 �63   �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   .+�5�      �� $ 
Horizontal=�6)   $��2��6B   .+�5�   "   �� -   5        $ GrainAmount��5�   *   $��50��6?   �� 5�   "   $��g��6,   #��[ 65   5�      �� 5�      $ 
Horizontal=�6)   $��4��6B   .+�5�   "   �� -   5         ��Name$ AddGrainItem��5�   )   $	��Add��6?   $��0 0 10 0q�6M   �� 5�   "   -   5        $ RemoveGrains��5�   )   $��Remove��6?   5�      '   !BrewUI.UserControls.AutoSearchBox  5�      $��0��6/      
SearchList 5�   /   �� *GrainSearchList+   SelectedItem 5�   /   �� $ SelectedGrainName�$ OneWayToSource=�5�      �� 5�      $��0 10 0 0q�6K   ��6'   �� *AddedGrains+��5�      �� 5�   "   .R�5�   &   �� $ Vertical=�61    K� IsItemsHost . 6H   5�   #   ��5�      �� 5�   "   .R�5�   &   �   GrayDark#��[ 6+   $��0 2 0 0q�6R    5�   *   �� 5�   .   $��5 �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?   �� 5�   .   $��5 �6?    5�   *   �� 5�   .   $��30 �6<   .+�5�   *   �� $��2��6J   $��10 0 0 0q�5�   8   
�������$��Center=�6]   ��5�   4   �� *name+5�   ]   �� 5�   *   $��4��6J   $��40��6Z    �� TextAlignment$ Center=�6e   $��10 0 0 0q�5�   8   $��Center=�6J   ��5�   2   �� *amount+5�   J   �� 5�   *   $��g��64   $��5��6=   $��Center=�6M   
�������$��5 0 0 0q�6{   �� 5�   *   $��6��6P   $��Center=�6`    d� 	IsChecked 63   �� *remove+6`   5�   '   5�   #   5�      5�      � 5�      $��0��6   $��4��6+    5�      �� 5�      $
��Auto �6,   �� 5�      $��10 �6,   �� 5�      $
��Auto �6,   �� 5�      $��10 �6,   �� 5�      $
��Auto �6,    5�      �� 5�      $��* �6/   .+�5�      �� $��Add mash steps��6$   #��[ 6:   � 5�      $��2��6    5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   .+�5�      �� $ 
Horizontal=�6)   $��0��6B   .+�5�   "   �� $��Step name: ��6,   $��Center=�6?   �� 5�   "   -   5        $ InputMashName��5�   *   $	��100��6A   $��0 0 10 0q�6M   )   #BrewUI.UserControls.TextBoxWithUnit  5�   "      BoxLabel$ Temperature: ��65      BoxValueWidth$ 40Q�6�      BoxUnit$ c��6�   $��Center=�6�   $��0 0 10 0q�6�      BoxValue 6N   �� $ InputMashTemp�$ TwoWay=�6�     5�   "   $ 
Duration: ��65   $ 40Q�6   $	 min��6�   $��Center=�6�    6K   �� $ InputMashDur�$ TwoWay=�6�   5�      �� 5�      $ 
Horizontal=�6)   $��2��6B   .+�5�   "   �� -   5        $ AddMashStep��5�   )   $	��Add��6>   $��0 0 10 0q�6L   �� 5�   "   -	   5        $ RemoveMashSteps��5�   )   $��Remove��6B   5�      5�      �� 5�      $��0 10 0 0q�6H   $��4��6Z   $��7��6g   ��6'   �� *
MashList+��5�      �� 5�   "   .R�5�   &   �� $ Vertical=�61    . 6H   5�   #   ��5      �� 5  "   .R�5  &   � #��[ 6+   $��0 2 0 0q�6R    5  *   �� 5  .   $��5 �6?   �� 5  .   $	��200 �6?   �� 5  .   $��* �6?   �� 5  .   $
��Auto �6?   �� 5  .   $
��Auto �6?   �� 5	  .   $��* �6?   �� 5
  .   $
��Auto �6?   �� 5  .   $��* �6?   �� 5  .   $
��Auto �6?   �� 5  .   $��5 �6?    5  *   �� 5  .   $��30 �6<   .+�5  *   �� $��1��6N   $��10 0 0 0q�5  8   
�������$��Center=�6]   ��5  4   �� *
stepName+5  ]   �� 5  *   $��3��6L   $��40��6\   $ Center=�6g   $��10 0 0 0q�5  8   $��Center=�6J   ��5  2   �� *
stepTemp+5  J   �� 5  *   $��g��64   $��4��6=   $��Center=�6M   
�������$��5 0 0 0q�6{   �� 5  *   $ BoilTimeText��62   $��6��6f   $��Center=�6v   $��60��6�   $ Center=�6�   ��6H   �� *stepDuration+6�   �� 5  *   $��Center=�6P   $��8��6k    63   �� *remove+6k   5  '   5  #   5     5�     5�     5�     5�     �� 5�  
   -
   5        $ SpargeContainer��5�     $��4��6*   $��0��67   $��3��6G   ._�5�     9�  .  ��5�     �� 5�     $��Sparge��6    $��0��6/   #��[  6<   .��5�     �  5�     �� 5�     $
��Auto �6+   �� 5�     $��* �6+    5�     �� 5�     $
��Auto �6(   �� 5�     $��20 �6(   �� 5�     $
��Auto �6(   .+�5�     �� $ 
Horizontal=�6!   $��2��5�     .+�5�     �� $��Temperature: ��6$   $��Center=�69   �� 5�     -   5        $ 
SpargeTemp��5�  "   $��30��66   �� 5�     $	��ºC��6$   #��[ 63   �� 5�     $��
Duration: ��6$   $��30 0 0 0q�66   $��Center=�6H   �� 5�     -   5        $ 	SpargeDur��5�  "   $��30��65   �� 5�     $	��min��6$   #��[ 6/   5�     5�     5�     5�     �� 5�  
   -   5        $ BoilContainer��5�     $��6��6(   $��0��65   ._�5�     9� ��5�     �� 5�     $
��Boil��6    #��[  6,   .��5�     �  5�     �� 5�     $��* �6+    5�     �� 5�     $
��Auto �6(   �� 5�     $��20 �6(   �� 5�     $
��Auto �6(   �� 5�     $��20 �6(   �� 5�     $
��Auto �6(   .+�5     �� $ Vertical=�6!   $��0��68   $��2��6H   .+�5     �� $��Add hops��6$   #��[ 64   $��0 0 0 10q�6U   � 5      5     �� 5  "   $
��Auto �63   �� 5  "   $��5 �63   �� 5  "   $
��Auto �63   �� 5	  "   $��5 �63   �� 5
  "   $
��Auto �63   �� 5  "   $��* �63   �� 5  "   $
��Auto �63   .+�5       $��0��6�    6/   �� *HopsSearchList+ 6U   �� $ SelectedHopsName�$ OneWayToSource=�6�   �� 5     $ 
Horizontal=�6)   $��2��6B   .+�5  "   �� -   5        $ 
HopsAmount��5  *   $��50��6>   �� 5  "   $��g��6,   #��[ 65   5     �� 5     $ 
Horizontal=�6)   $��4��6B   .+�5  "   �� -   5        $ HopsBoilTime��5  *   $��50��6@   �� 5  "   $	��min��6,   #��[ 67   5     �� 5      $ 
Horizontal=�6)   $��6��6B   .+�5!  "   �� -   5        $ AddHopsItem��5!  )   $	��Add��6>   $��0 0 10 0q�6L   �� 5"  "   -   5        $ 
RemoveHops��5"  )   $��Remove��6=   5#     5$     �� 5%     $��0 10 0 0q�6I   ��6'   �� *	AddedHops+��5&     �� 5'  "   .R�5(  &   �� $ Vertical=�61    . 6H   5)  #   ��5+     �� 5,  "   .R�5-  &   � #��[ 6+   $��0 2 0 0q�6R    5.  *   �� 5/  .   $��5 �6?   �� 50  .   $	��200 �6?   �� 51  .   $��* �6?   �� 52  .   $
��Auto �6?   �� 53  .   $
��Auto �6?   �� 54  .   $��* �6?   �� 55  .   $
��Auto �6?   �� 56  .   $��* �6?   �� 57  .   $
��Auto �6?   �� 58  .   $��5 �6?    5:  *   �� 5;  .   $��30 �6<   .+�5=  *   �� $��1��6J   $��10 0 0 0q�5>  8   
�������$��Center=�6]   ��5=  4   �� *Name+5>  ]   �� 5?  *   $��3��6J   $��40��6Z   $ Center=�6e   $��10 0 0 0q�5@  8   $��Center=�6J   ��5?  2   �� *Amount+5@  J   �� 5A  *   $��g��64   $��4��6=   $��Center=�6M   
�������$��5 0 0 0q�6{   �� 5B  *   $ BoilTimeText��62   $��6��6b   $��Center=�6r   $��60��6�   $ Center=�6�   ��6H   �� *
BoilTime+6�   �� 5C  *   $��Center=�6P   $��8��6k    63   �� *Remove+6k   5D  '   5E  #   5G     5H     5J     5K     5M     �� 5P  
   -   5        $ CooldownContainer��5P     $��8��6,   $��0��69   $��3��6I   ._�5Q     9� ��5R     �� 5S     $��Cooldown��6    $��0��61   $��2��6>   #��[  6R   .��5U     �  5V     �� 5W     $
��Auto �6+   �� 5X     $��* �6+    5Z     �� 5[     $
��Auto �6(   �� 5\     $��20 �6(   �� 5]     $
��Auto �6(   .+�5a     �� $ 
Horizontal=�6!   $��2��5b     .+�5c     �� $��Target temperature: ��6$   $��Center=�6@   �� 5d     -   5        $ CDTargetTemp��5d  "   $��30��68   �� 5e     $�� ºC ��6$   #��[ 65   �� 5f     $��Run cooldown��6#   $��30 0 0 0q�6\    6:   �� *RunCooldown+6\   5g     5h     5i     5k     �� 5m  
   $ 
Horizontal=�6   $��Right=�6.   $��10��5n     $��0��6#   .+�5o     �� -   5         ��Name$ CurrentStep��5o     $	��100��6-   �� 5p     -   5        $ StartButton��5p     $��30��6*   $	��Top=�66   ��5q     �� 5r     $ 
Horizontal=�6!   .+�5s     �� $��Start session��6$   '  !MaterialDesignThemes.Wpf.PackIcon  5t     �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ NavigateNext=�62   $��0 1 0 0q�6F   5u     5w     5x     5y     5z     