   M S B A M L   `   `   `  ���� I  EAtendimentoPDV, Version=1.1.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles,Artebit.Restaurante.AtendimentoPDV.Controles  @ <VKbrd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,"clr-namespace:VKbrd;assembly=VKbrdVKbrd 6    0Artebit.Restaurante.AtendimentoPDV.Telas.Inicial   H	Controles:clr-namespace:Artebit.Restaurante.AtendimentoPDV.Controles  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4VKbrd"clr-namespace:VKbrd;assembly=VKbrd       +� FocusedElement  ��  �� ElementName$ txt_mesa��.���� .+�� .+�=   7Artebit.Restaurante.AtendimentoPDV.Controles.Cabecalho1     Titulo$ Atendimento��� $	��990��$	��650��$��15 0 0 0q� � ColumnDefinitions �� $	��690 ��� $	��300 � � RowDefinitions �� $��30 ��� $	��400 ��� $	��210 �.+��� -    ��Name$ txtNomeUsuario��$
��Left=�$
��Wrap=�$��
Usuário: ��$��20 7 0 0q�$��16(�
�������$	��Top=�$��1��$��0����  �� CornerRadius$ 3��
���iii�$��3q�$��10q�
���iii�$��1��$��1�� �� Padding$ 10q�$	��260��$	��380��._�� -    �Name$ 
grid_login��.+�  VKbrd.KeyPad2  -   	  Name$	 keyPad1��$
��Left=�$��Bottom=�$	��260��$	��235��$��0 0 0 0q��� -   $��txt_mesa��$��40��$
��Left=�$��0,50,0,0q�$��23(�$	��Top=�$	��235���� -   $��titulo��$��Informe o número da mesa��
�������$��32��$��Center=�$��Segoe UI)�$��Normal"�$��18(�$	��Top=�$��0 0 0 0q��� -   $��imagem��$��5��$	��220��$
��Fill=�$	��Top=�$��Center=�$2��,/AtendimentoPDV;component/Img/tracoLetra.png��$��7,28,7,0q��� $��1��$��2��.+��� -   
 ��Name$
 bt_mesa��$
��Mesa�� J� Opacity$ 1Q�|�.  $��0 10 0 0q�$��45��$	��150���� -   $
 bt_conta��$��Conta��$ 1Q�|�.  $��0 10 0 0q�$��45��$	��150���� -	   $
 bt_sair��$
��Sair��$ 1Q�|�.  $��0 10 0 0q�$��45��$	��150���� -
    ��Name$ VerticalScroller��$��0��$��3��$��	0 15 0 10q�   FavsScrollViewer#��[  $��Disabled=���. 
����  �.���� -    ��Name$ 	grid_mesa������ .R�A�  A� Columns$ 5������ .R��� -   $��0,0,0,-4q�|�.  $��	0 10 0 10q���Z� ���� *nuMesa+.���� .+��� $��imagem��$
��Fill=�$��40��$��70������ *icone+�� $��Center=�$
��Bold"�$��18(����� *nuMesa+