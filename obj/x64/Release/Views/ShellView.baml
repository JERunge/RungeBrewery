   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BrewUI.ViewsBrewUI.Views      BrewUI.Views.ShellView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  &localclr-namespace:BrewUI.Views  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity    � Title$  Brewery 2020��$
��1000��$
��1000��$	��857��$	��857�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�   	GrayLight#��[  $��0q� � 
ResizeMode$ CanResizeWithGrip=� � AllowsTransparency .  � WindowState ��  �� Path$ CustomWindowState� �� Mode$ TwoWay=� � 	Resources -  'MahApps.Metro.Controls.MetroProgressBar	 �� 
TargetType
 �� Property �� Value �� Triggers �� Binding �� Value��%   '        �� "	  .��� 
 � y�$ Hidden=� ��  �� *ConnectionIsBusy+ true.a��� 
 � y�$ Visible=�.���  � ColumnDefinitions �� $	��150 ��� $��20 ��� $��* ��� $��20 � � RowDefinitions �� $��70 ��� $��20 ��� $��* ��� $��20 ��� $��75 ��� $��20 �.+�� $��0��$��0��  AccentColor#��[ .+���  �� Orientation$ 
Horizontal=�.+��� $��RUNGE BREWERY��$��Center=� �� TextAlignment$ Center=�
�������$
��Bold"�$��20 0 0 0q�$��14(�� -    �Name$ 
HeaderGrid��$��0��$��1��$��5��  GrayDark#��[ $��
0,0,-0.4,0q�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *MashClickedCommand+.+��� $ 
Horizontal=�$��Right=�.+��� -    ��Name$ MinimizeButton��$��0q�$	��0 0q�#��[ ��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ WindowMinimize=�$��20��$��30��
��������� -   $ RestoreButton��$��0q�$	��0 0q�#��[ ��  $ WindowRestore=�$��20��$��30��
��������� -   $ CloseButton��$��0q�$��0 0 20 0q�$	��0 0q�#��[ ��  $ Close=�$��20��$��30��
��������� $ Vertical=�$��1��$��0��$��6��#��[ $��
0,0,0,-0.2q�   
MenuButton��%�   &	       7  �� "	 ��.��� 
 � ��8   �� 
 � ��
 ������� 
 � ��$ 15 0 0 0q��� 
 � ��$
 Left=��� 
 � ��$ Center=��� 
 � ��$ 0q��� 
 � ��$ 12(��� 
 � ��$ 70��.+��� -   $ LoadSessionSettings������ "	 �� �� BasedOn# [  ��  �� *ActiveWindow+ 	SessionVM.a��� 
 � ��  ButtonSelected# [ ���� $ 
Horizontal=�.+�  $
 beer=�$��0 0 10 0q�$��Center=��� $��Brew Session���� -   $ 	NewRecipe��#��[ ���� $ 
Horizontal=�.+�  $ NotePlus=�$��0 0 10 0q�$��Center=��� $��
New recipe���� -   $ 
OpenRecipe��#��[ ���� $ 
Horizontal=�.+�  $ FolderOpenOutline=�$��0 0 10 0q�$��Center=��� $��Open recipe���� -   $ 
SaveRecipe��#��[ ���� $ 
Horizontal=�.+�  $ ContentSaveOutline=�$��0 0 10 0q�$��Center=��� $��Save recipe���� -	   $ ImportRecipe��#��[ $��0 0 0 1q����� $ 
Horizontal=�.+�  $ Import=�$��0 0 10 0q�$��Center=��� $��Import recipe���� -
   $ 
LoadManual������ "	 ��# [  ��  �� *ActiveWindow+ ManualVM.a��� 
 � ��# [ ���� $ 
Horizontal=�.+�  $ Screwdriver=�$��0 0 10 0q�$��Center=��� $��Manual���� -   $ LoadSettings��#��[ ���� $ 
Horizontal=�.+�  $ SettingsOutline=�$��0 0 10 0q�$��Center=��� $��Settings���� $��2��$��2��$��Hidden=�$��Hidden=�.���� -    ��Name$ 
ActiveItem��$��0 0 0 0q�$��2��$��2���� $��1q�#��[  �� CornerRadius$ 3��$��4��$��2��$��1��
�������$��1��._��  �� $��20 ��� $��* ��� $��20 � �� $��20 ��� $
��Auto ��� $��20 � ��%�   '��       '�� y     �� "	 ��.��� 
 � ��
 ������� 
 � ��$ 1q��� 
 �@��CornerRadius$ 3���� 
 �@��Padding$ 5q��� "	 ��.��� 
 � ��$ Center=�.+�� $��1��$��1�� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $ 
Horizontal=�$��0��.+��� -   $ ConnectButton��$	��100������ *ConnectionText+  
�������$��20��$��30��$��Center=�$��20 0 0 0q� �� *BluetoothIcon+�� -    ��Name$ ConnectionStatus��$��75��   � IsIndeterminate . $��Center=�$
��10 0q�$	��100���� $ 
Horizontal=�$��2��.+�  $ 	WaterPump=�$��	40 0 10 0q�$��Center=��� -   $ 
PumpStatus��$��20��  $ Radiator=�$��	40 0 10 0q�$��Center=��� -   $ HeaterStatus��$��20��