   M S B A M L   `   `   `  ���� A  =BrewUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BrewUI.UserControlsBrewUI.UserControls  &     BrewUI.Views.SessionSettingsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Aa9http://schemas.microsoft.com/expression/2010/interactions  *uc!clr-namespace:BrewUI.UserControls  $
��Auto��$��Stretch=�  � 	Resources   �� 
TargetType �� Property �� Value��%�   '��       '�� =     �� " ��.���  � ��$ 18����  � ��$ Center=��� " ��.���  �@��Padding$ 10q���  � ��
 �������  � ��
 �������  � ��$ 1q���  �@��CornerRadius$ 3��.���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $��* �.+��� $��0��$��Stretch=�._���  �� Orientation$ Vertical=�  ��%3   '��       �� " ��.���  � ��$ 0 0 0 10q�.+�� $��0 0 15 0q� �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��
Properties��   Header1#��[  $��0��$��0 0 0 15q��  � Orientation$ 
Horizontal=�.+��� $��0 0 10 0q�._��� $ Vertical=�.+��� $
��Name���� -    ��Name$ SessionName��$	��220��$��20��$��Italic$�.������ 	 �� Triggers	 �� 
 �� Binding
 �� *SessionNameBorder+ �� Value True.a���  � ��$ 1q��� 
 �� *SessionNameBorder+	 False.a���  � ��$ 0q�.���� $��0 0 10 0q�._��� $ Vertical=�.+��� $��
Batch size���� $ 
Horizontal=�.+��� -   $ 	BatchSize��$��30��$��18���� $�� liter��  	UnitBlock#��[ �� $��0 0 10 0q�._��� $ Vertical=�.+��� $��Method���� $	��100������ *BrewMethods+ �� SelectedItem ��  �� Path$ SelectedBrewMethod� �� Mode$ TwoWay=����� .R��� ���� *Name+�� ._��� $ Vertical=�.+��� $��Style���� $	��150������ *	StyleList+ �� $ SelectedBeerStyle�$ TwoWay=����� .R��� ���� *Name+�� -    ��Name$ MashContainer��$��2��$��0��._�9�  9� 
IsExpanded . ���� $
��Mash��#��[  .���  �� $��* � �� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto �.+��� $ Vertical=�$��0��$��2��.+��� $��
Add grains��  Header2#��[ $��0 0 0 10q��  �� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $ 
Horizontal=�$��2��.+��� -   $ GrainAmount��$��50���� $��g��#��[ �� $ 
Horizontal=�$��4��.+��� -    ��Name$ AddGrainItem��$	��Add��$��0 0 10 0q��� -   $ RemoveGrains��$��Remove��'   !BrewUI.UserControls.AutoSearchBox  $��0��   
SearchList �� *GrainSearchList+   SelectedItem �� $ SelectedGrainName�$ OneWayToSource=��� $��0 10 0 0q����� *AddedGrains+���� .R��� $ Vertical=� K� IsItemsHost . ���� .R��   GrayDark#��[ $��0 2 0 0q� �� $��5 ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��5 � �� $��30 �.+��� $��2��$��10 0 0 0q�
�������$��Center=����� *name+�� $��4��$��40�� �� TextAlignment$ Center=�$��10 0 0 0q�$��Center=����� *amount+�� $��g��$��5��$��Center=�
�������$��5 0 0 0q��� $��6��$��Center=� d� 	IsChecked �� *remove+� $��0��$��4�� �� $
��Auto ��� $��10 ��� $
��Auto � �� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto ��� $��10 ��� $
��Auto �.+��� $��Select mash steps��#��[ �� $��0��$��2��._��  �� $
��Auto ��� $
��Auto � �� $��30 ��� $
��Auto �.+��� -    ��Name$ AcidCheckBox��$��0��$��0��$��	Acid rest�� �� *AcidChecked+�� $ Vertical=�$��1��$��0��.+��� $��Temperature: ��$��0 0 0 5q��� $��	Duration:���� $ Vertical=�$��1��$��1��.+��� $ 
Horizontal=�.+��� -   $ InputAcidTemp��$��30��$��0 0 0 0q�$��Right=�$��1��$��4��{���  �� ElementName$ AcidCheckBox��$ 	IsChecked��� $�� ºC ��#��[ �� $ 
Horizontal=�.+��� -	   $ InputAcidDur��$��30��$
��Left=�$��1��$��2��{��� $ AcidCheckBox��$ 	IsChecked��� $�� min ��#��[ �� $��2��$��2��._��  �� $
��Auto ��� $
��Auto � �� $��30 ��� $
��Auto �.+��� -
   $ ProteinCheckBox��$��0��$��0��$��Protein rest�� �� *ProteinChecked+�� $ Vertical=�$��1��$��0��.+��� $��Temperature: ��$��0 0 0 5q��� $��	Duration:���� $ Vertical=�$��1��$��1��.+��� $ 
Horizontal=�.+��� -   $ InputProteinTemp��$��30��$��0 0 0 0q�$��Right=�$��1��$��4��{��� $ ProteinCheckBox��$ 	IsChecked��� $�� ºC ��#��[ �� $ 
Horizontal=�.+��� -   $ InputProteinDur��$��30��$
��Left=�$��1��$��2��{��� $ ProteinCheckBox��$ 	IsChecked��� $�� min ��#��[ �� $��4��$��2��._��  �� $
��Auto ��� $
��Auto � �� $��30 ��� $
��Auto �.+��� -   $ StarchCheckBox��$��0��$��0��$��Starch rest�� �� *StarchChecked+�� $ Vertical=�$��1��$��0��.+��� $��Temperature: ��$��0 0 0 5q��� $��	Duration:���� $ Vertical=�$��1��$��1��.+��� $ 
Horizontal=�.+��� -   $ InputStarchTemp��$��30��$��0 0 0 0q�$��Right=�$��1��$��4��{��� $ StarchCheckBox��$ 	IsChecked��� $�� ºC ��#��[ �� $ 
Horizontal=�.+��� -   $ InputStarchDur��$��30��$
��Left=�$��1��$��2��{��� $ StarchCheckBox��$ 	IsChecked��� $�� min ��#��[ �� $��6��$��2��._��  �� $
��Auto ��� $
��Auto � �� $��30 ��� $
��Auto �.+��� -   $ SpargeCheckBox��$��0��$��0��$��Mash out�� �� *SpargeChecked+�� $ Vertical=�$��1��$��0��.+��� $��Temperature: ��$��0 0 0 5q��� $��	Duration:���� $ Vertical=�$��1��$��1��.+��� $ 
Horizontal=�.+��� -   $ InputSpargeTemp��$��30��$��0 0 0 0q�$��Right=�$��1��$��4��{��� $ SpargeCheckBox��$ 	IsChecked��� $�� ºC ��#��[ �� $ 
Horizontal=�.+��� -   $ InputSpargeDur��$��30��$
��Left=�$��1��$��2��{��� $ SpargeCheckBox��$ 	IsChecked��� $�� min ��#��[ �� -   $ SpargeContainer��$��4��$��0��$��3��._�9�  .  ���� $��Sparge��$��0��#��[  .���  �� $
��Auto ��� $��* � �� $
��Auto ��� $��20 ��� $
��Auto �.+��� $ 
Horizontal=�$��2��.+��� $��Temperature: ��$��Center=��� -   $ 
SpargeTemp��$��30���� $	��ºC��#��[ �� $��
Duration: ��$��30 0 0 0q�$��Center=��� -   $ 	SpargeDur��$��30���� $	��min��#��[ �� -   $ BoilContainer��$��6��$��0��._�9� ���� $
��Boil��#��[  .���  �� $��* � �� $
��Auto ��� $��20 ��� $
��Auto ��� $��20 ��� $
��Auto �.+��� $ Vertical=�$��0��$��2��.+��� $��Add hops��#��[ $��0 0 0 10q��  �� $
��Auto ��� $��5 ��� $
��Auto ��� $��5 ��� $
��Auto ��� $��* ��� $
��Auto �.+�  $��0�� �� *HopsSearchList+ �� $ SelectedHopsName�$ OneWayToSource=��� $ 
Horizontal=�$��2��.+��� -   $ 
HopsAmount��$��50���� $��g��#��[ �� $ 
Horizontal=�$��4��.+��� -   $ HopsBoilTime��$��50���� $	��min��#��[ �� $ 
Horizontal=�$��6��.+��� -   $ AddHopsItem��$	��Add��$��0 0 10 0q��� -   $ 
RemoveHops��$��Remove���� $��0 10 0 0q����� *	AddedHops+���� .R��� $ Vertical=� . ���� .R�� #��[ $��0 2 0 0q� �� $��5 ��� $	��200 ��� $��* ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��5 � �� $��30 �.+��� $��1��$��10 0 0 0q�
�������$��Center=����� *Name+�� $��3��$��40��$ Center=�$��10 0 0 0q�$��Center=����� *Amount+�� $��g��$��4��$��Center=�
�������$��5 0 0 0q��� $��6��$��Center=�
����������� *
BoilTime+�� $��Center=�$��8�� �� *Remove+�� -   $ CooldownContainer��$��8��$��0��$��3��._�9� ���� $��Cooldown��$��0��$��2��#��[  .���  �� $
��Auto ��� $��* � �� $
��Auto ��� $��20 ��� $
��Auto �.+��� $ 
Horizontal=�$��2��.+��� $��Target temperature: ��$��Center=��� -   $ CDTargetTemp��$��30���� $�� ºC ��#��[ �� $��Run cooldown��$��30 0 0 0q� �� *RunCooldown+�� $ 
Horizontal=�$��Right=�$��10��$��0��.+��� -    ��Name$ CurrentStep��$	��100���� -   $ StartButton��$��30��$	��Top=����� $ 
Horizontal=�.+��� $��Start session��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ NavigateNext=�$��0 1 0 0q�