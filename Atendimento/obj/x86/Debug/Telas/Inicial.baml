   M S B A M L   `   `   `  ����I  EAtendimentoPDV, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles,Artebit.Restaurante.AtendimentoPDV.Controles  5      @ <VKbrd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,"clr-namespace:VKbrd;assembly=VKbrdVKbrd 5   K   6    0Artebit.Restaurante.AtendimentoPDV.Telas.Inicial   5      H	Controles:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   I   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4VKbrd"clr-namespace:VKbrd;assembly=VKbrd     6K     +� FocusedElement  5   H   ��  �� ElementName$ txt_mesa��.��5      �� .+�5   
   � .+�5	      =   7Artebit.Restaurante.AtendimentoPDV.Controles.Cabecalho1     Titulo$ Atendimento��6#   5
      � 5   
   $	��990��6   $	��650��6   $��15 0 0 0q�6.    � ColumnDefinitions 5      �� 5      $	��690 �6#   �� 5      $	��300 �6#    � RowDefinitions 5      �� 5      $��30 �6    �� 5      $	��400 �6    �� 5      $	��210 �6    .+�5      �� -   5         ��Name$ txtNomeUsuario��5      $
��Left=�60   $
��Wrap=�6K   $��
Usuário: ��6_   $��20 7 0 0q�6p   $��16(�6�   
�������$	��Top=�5      $��1��60   $��0��6@   �� 5       �� CornerRadius$ 3��6   
���iii�$��3q�6<   $��10q�6P   
���iii�$��1��5      $��1��6%    �� Padding$ 10q�62   $	��260��6?   $	��380��6K   ._�5      � -   5         �Name$ 
grid_login��5      .+�5        VKbrd.KeyPad2  -   5        	  Name$	 keyPad1��5   ?   $
��Left=�6$   $��Bottom=�6P   $	��260��6k   $	��235��5   $   $��0 0 0 0q�60   �� 5      -   5        $��txt_mesa��5   W   $��40��6   $
��Left=�6*   $��0,50,0,0q�6E   $��23(�6g   $	��Top=�5       $	��235��66   6B   �� 5"      -   5        $��titulo��5"      $��Informe o número da mesa��6*   
�������$��32��6`   $��Center=�5#      $��Segoe UI)�69   $��Normal"�6O   $��18(�6c   $	��Top=�6q   $��0 0 0 0q�5$      �� 5%      -   5        $��imagem��5%      $��5��6*   $	��220��65   $
��Fill=�6A   $	��Top=�6P   $��Center=�5&      $2��,/AtendimentoPDV;component/Img/tracoLetra.png��69   $��7,28,7,0q�6o   5'      5)      �� 5+      $��1��6   $��2��6)   .+�5,      �� -   5        
 ��Name$
 bt_mesa��5,   4   $
��Mesa��6    J� Opacity$ 1Q�6(   |�.  $��0 10 0 0q�6W   $��45��6i   $	��150��5-      6%   �� 5/      -   5        $
 bt_conta��5/   5   $��Conta��6   $ 1Q�6)   |�.  $��0 10 0 0q�6Y   $��45��6k   $	��150��50      6%   �� 52      -	   5        $
 bt_sair��52   4   $
��Sair��6   $ 1Q�6(   |�.  $��0 10 0 0q�6W   $��45��6i   $	��150��53      6%   54      �� 56      -
   5         ��Name$ VerticalScroller��57      $��0��56      $��3��6+   $��	0 15 0 10q�6<      FavsScrollViewer#��[  6O   $��Disabled=�57   5   ��. 
����  �.��58      �� -   5         ��Name$ 	grid_mesa��58      ��5:      �� 5;      .R�5<      A�  A� Columns$ 5��6*   5=      ��5@      �� 5A      .R�5B      �� -   5        $��0,0,0,-4q�5B   %   |�.  $��	0 10 0 10q�5C   I   ��5B   8   Z� ��5C   %   �� *nuMesa+.��5D   "   �� .+�5E   &   �� $��imagem��6,   $
��Fill=�6:   $��40��6I   $��70��6U   ��6`   �� *icone+�� 5F   &   $��Center=�6G   $
��Bold"�6d   $��18(�5G   ,   ��5F   ,   �� *nuMesa+5G   ,   5H   #   5I      5J      5M      5N      5Q      5R      5S      