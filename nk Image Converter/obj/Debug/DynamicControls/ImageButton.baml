   M S B A M L   `   `   `  ����  nk Image Converter4    .nk_Image_Converter.DynamicControls.ImageButton   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          >�Name$  ImageButtonContainer��5
      $	��135��5      $	��116��6   $��0q�5	       J� SnapsToDevicePixels . .��5      � -   5         �Name$ ControlContainer��5      ���#d4dce6 .  J� Effect 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         Opacity$	 0.2Q�6    � RowDefinitions 5   
   �� 5      $��15 �6   �� 5      $��* �6   .+�5   
   � -   5        $ Header��5      $��0��6    .  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��15 �6#   .+�5      �� -   5         ��Name$ 	ImageName��5      $	��100��6'   $��Center=�63   $��10(�6P   $��0q�6^   $��0q�6j    . .��5      �� -   5         ��Name$ ImageNameTextBlock��5      $��Bottom=�68   $��15��6S   $��CharacterEllipsis=�6_   	 �� ClearTypeHint$	 Enabled=�6�    . sdcsdc6�   5      �� 5      -   5        
 ��Name$ 
 ImageEnableDisableCheckBox��5       d� 	IsChecked$
 TrueY�6;   $��0��6L   $��Center=�6Y   $��Center=�6v   
�����_����#d4dce6$��2��6�   $��	119,0,1,0q�6�    . 6�   5      �� 5   
   -   5         ��Name$ ImageHolderButton��5      $��1��6,   $��0q�69    . ��5      �� 5        �� 
TargetType" ��6   .�5!      ��  �� Property � �� �� Value �#D4DCE663   �� 5"       � �� 5#      �� 5$       �� 
TargetType" ��6.   .R�5%   "   �� #��z��6)   ._�5&   &   �� $
��Left=�67   $��Center=�6R   5'   #   5(      5*       �� Triggers 5+      P� 5,       P� Property �@J�IsMouseOver P� Value . . �5-      ��  � �� �#bec6cf6;   �� 5.       �@�Cursor$
 Hand��67   5/      51      .��53      �� -   5         ��Name$ PreviewImage��53      $
��Fill=�6*   54      55      56      