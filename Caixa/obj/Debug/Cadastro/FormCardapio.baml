   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormCardapio   5      m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n	 jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 R
 NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006  
   5      @d2http://schemas.microsoft.com/expression/blend/2008  
   5      )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5	      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5
      $��	Cardápio��5      
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5   '    � 	Resources 5      (  "Telerik.Windows.Controls.RadButton �� 
TargetType+  %Telerik.Windows.Controls.StyleManager �� Property �� Value*  $Telerik.Windows.Controls.RadGridView*  $Telerik.Windows.Controls.RadComboBox��%  '        '  T     '  �     �� 5   
   "  6   .�5      ��  �@ Theme$ Windows7 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Metro 6;   5      �� 5   
   "  6   .�5      ��  �@ Theme$ Summer 6;   5      5      .��5      � $	��400��6   $	��400��6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   ��5   
   �� 5        �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5%   
   ,	  &Telerik.Windows.Controls.RadTabControl	  -   5        $��tabControl1��5%   n   $	��350��6    $	��810��6-   $��Center=�69   $	��Top=�6V   ;
  5Telerik.Windows.Controls.TabControl.RadTabControlBase	 
  BackgroundVisibility$	 	Collapsed=�6�   .@�5&      )  #Telerik.Windows.Controls.RadTabItem  -   5        $��abaIdentificacao��5&   8   $��Identificação��6!   $	��110��6P   ���#EEEEEE.��5'      � $��3q�6    5(      �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      $��5* �6(   �� 5/      
 � ColumnDefinitions
 51      �� 52      $	��99* �6+   �� 53      $
��454* �6+   �� 54      $
��198* �6+   .+�59      �� $��Nome :��6    $��Center=�6.   $��3q�6J   �� 5:      $��Ativo :��6    $��2��6/   $��Center=�6<   $��Normal"�6W   $��3q�6k   �� 5;      $��Horário Ativo:��6    $��1��66   $��Center=�6C   $��3q�6^   �� 5<      $��
Dia Todo :��6    $��3��62   $��Center=�6?   $��Normal"�6Z   $��3q�6n   �� 5A      -   5        $��imgImagemView��5A      $��2��61   $��2��6A   $��4��6U   $��2��6f   $��14q�6s   �� 5F      -   5         ��Name$ txtNome��5F      $
��Left=�6/   $��1��6J   $	��374��6Z   $��25��6f   $	��0,2q�6r   �� 5G      -   5        $��ckbAtivo��5G      $��Center=�6/   $
��Left=�6J   $��2��6e   $��1��6r   $��3q�6�   /  )Telerik.Windows.Controls.RadMaskedTextBox  5I      -   5          Name$ txtHor1��5I   /   $
��Left=�6@      Culture$ pt-BR��6b      MaskType$ DateTime=�6r      Mask$ HH:mm��6�   $
��Left=�6�   $��1��6�   $��1��6�   $��42��6�   $��25��6�   $	��0,2q�6�   �� 5J      $��à��6    $��10��6)   $��1��64   $��1��6A   $��Center=�6Q   $
��Left=�6l   $��14(�6�   $��Normal"�6�   $��55 3 3 3q�6�     5L      -   5        $ txtHor2��5L   /   $
��Left=�6@   $ pt-BR��6b   $ DateTime=�6r   $ HH:mm��6�   $
��Left=�6�   $��1��6�   $��1��6�   $��42��6�   $��25��6�   $��75,0,0,0q�6�   �� 5M      -   5        $��
ckbDiaTodo��5M      $��Center=�61   $
��Left=�6L   $��3��6g   $��1��6t   $��3q�6�   � 5O      $��2��6   $��5��6/   $��
10,0,188,0q�6<    5P      �� 5Q      �� 5R      �� 5S      �� 5T      �� 5U      �� 5V      $��* �6,   
 5X      �� 5Y      �� 5Z      $��2* �6/   �� 5[      �� 5\      $��3* �6/   .+�5`      �� $��Dias da semana ativo:��6$   $��0��6A   $��0��6N   $��2��6^   $
��Left=�6r   $��0q�6�   �� 5c      -	   5        $��
ckbSegunda��5c   #   $��Center=�65   $��Center=�6P   $��1��6m   $��3q�6z   �� 5d      $��Segunda��6$   $��1��63   $��Center=�6C   $��1��6_   $��Normal"�6l   $��3q�6�   �� 5e      -
   5        $��	ckbQuarta��5e   #   $��Center=�64   $��Center=�6O   $��2��6l   $��3q�6y   �� 5f      $��Quarta��6$   $��1��62   $��2��6B   $��Center=�6O   $��Normal"�6j   $��3q�6~   �� 5g      -   5        $��ckbSexta��5g   #   $��Center=�63   $��Center=�6N   $��3��6k   $��3q�6x   �� 5h      $��Sexta��6$   $��1��61   $��3��6A   $��Center=�6N   $��Normal"�6j   $��3q�6~   �� 5i      -   5        $��
ckbDomingo��5i   #   $��Center=�65   $��Center=�6P   $��4��6m   $��3q�6z   �� 5j      $��Domingo��6$   $��1��63   $��4��6C   $��Center=�6P   $��Normal"�6l   $��3q�6�   �� 5l      -   5        $��ckbTerca��5l   #   $��Center=�63   $��Center=�6N   $��1��6k   $��2��6x   $��3q�6�   �� 5m      $��Terça��6$   $��3��61   $��1��6A   $��Center=�6N   $��Normal"�6j   $��3q�6~   �� 5n      -   5        $��	ckbQuinta��5n   #   $��Center=�64   $��Center=�6O   $��2��6l   $��2��6y   $��3q�6�   �� 5o      $��Quinta��6$   $��3��62   $��2��6B   $��Center=�6O   $��Normal"�6k   $��3q�6   �� 5p      -   5        $��	ckbSabado��5p   #   $��Center=�64   $��Center=�6O   $��3��6l   $��2��6y   $��3q�6�   �� 5q      $��Sábado��6$   $��3��62   $��3��6B   $��Center=�6O   $��Normal"�6j   $��3q�6~   5s      5u      5v        5w      -   5        $��abaItens��5w   0   $��Itens��6!   $	��110��6@   ���#EEEEEE.��5x      �  5y         Headerstyle9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� 
TargetType ��Name��%!
  &	        �� 5z      "  64   .�5{      ��  � ��$ 25��67   �� 5|       � �� 5}   "   �� 5~   &   "  66   .R�5�   *   �� $ borda1��61   ���#B68944���#49181A$��0 0 0 0q�5�   %   ._�5�   .   � 
 5�   2   �� 5�   6   $��* �6G   �� 5�   6   $��40 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��55 �6G   �� 5�   6   $��60 �6G   �� 5�   6   $��70 �6G   �� 5�   6   $��70 �6G   .+�5�   2   �� ���#49181A$��1q�5�   -   $��0��5�   -   ._�5�   6   �� $��0 0 0 0q�6<   
�������$��Center=�5�   4   $��11(�5�   4   $
��Nome��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��1��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Ativo��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��2��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Segunda��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��3��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Terça��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��4��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Quarta��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��5��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Quinta��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��6��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Sexta��5�   4   5�   3   �� 5�   2   ���#49181A$��1q�5�   -   $��7��5�   -   ._�5�   6   �� $��Center=�6<   
�������$��Center=�5�   4   $��11(�5�   4   $��Sábado��5�   4   5�   3   �� 5   2   ���#49181A$��1q�5  -   $��8��5  -   ._�5  6   �� $��Center=�6<   
�������$��Center=�5  4   $��11(�5  4   $��Domingo��5	  4   5
  3   �� 5  2   ���#49181A$��1q�5  -   $��9��5  -   ._�5  6   �� $��Center=�6<   
�������$��Center=�5  4   $��11(�5  4   $��Hora Inicio��5  4   5  3   �� 5  2   ���#49181A$��1q�5  -   $��10��5  -   ._�5  6   �� $��Center=�6<   
�������$��Center=�5   4   $��11(�5!  4   $��Hora Fim��5"  4   5#  3   5%  /   5&  +   5)  '   5+     5,     5-     .+�5/     �� -   5        $��lbGrupo��5/  g   $��Grupo :��6   $��28��6.   $
��Left=�6:   $��4,12,0,0q�6U   $	��Top=�6v   -  'Telerik.Windows.Controls.RadRadioButton  51     -   5        $��btBar��51  �   $��Cozinha��6-   $��28��6?   $
��Left=�6K    d� 	IsChecked$
 TrueY�6f   $��	59,11,0,0q�6w   $	��Top=�6�   $��68��6�    	� 	GroupName$ Grupo��6�   6�     52     -   5        $��	btCozinha��52  �   $	��Bar��6-   $ Grupo��6;   $��28��6M   $
��Left=�6Y   $ FalseY�6t   $��
133,11,0,0q�6�   $	��Top=�6�   $��68��6�   6�   �� 54     -   5        $��lbCategoria��54  s   $��Categoria 1��6   $��32��62   $��Center=�6>   $��351,40,351,251q�6[   $��Center=�6�   $��16(�6�   $
��Bold"�6�   ���#B689446�     56     -   5        $��	gridDados��56  X   $
��Left=�6*   $��	0,100,0,0q�6E   $	��Top=�6i   $	��798��6�   $	��223��6�   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .     CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�6�      CanUserReorderColumns .     MinColumnWidth$ 50Q�6     ShowGroupPanel .     ShowGroupFooters .     SelectionMode$ Extended=�6X     ActionOnLostFocus$
 None=�6�     CanUserDeleteRows .     CanUserInsertRows .     Columns 57     3  -Telerik.Windows.Controls.GridViewSelectColumn  58     1  +Telerik.Windows.Controls.GridViewDataColumn  59     -  'Telerik.Windows.Controls.GridViewColumn    IsFilterable  .  !   
IsReadOnly! . 6  0Telerik.Windows.Controls.GridViewBoundColumnBase"   Header$" Produto��6�   #   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$
# 455* 6�   $   DataMemberBinding$ 69   �� *EPRODUTO.nome+6�     5:     $" Preço��69     .  ! . $	# 100 6�   %   
UniqueName$% Preco��6�   $ 6]   �� *	nuPreco+&   CellTemplate& 5;  "   �� 5<  &   .R�5=  *   � .+�5>  .     '  Name$' 
ComboPreco��6B   -   5        {�. ( �� SelectedIndex( 5>  �   �� *	nuPreco+.@�5?  2   .  (Telerik.Windows.Controls.RadComboBoxItem  $��-��6J     5@  2   $��Preço 1��6J     5A  2   $��Preço 2��6J     5B  2   $��Preço 3��6J   5C  /   5D  +   5E  '   5G     5I       5J     -   5        $��btAdd��5J  l   $��28��6(   $��32��64   $
��Left=�6?   $��0,70,0,0q�6Z   $	��Top=�6y   ��5K     �� 5L     $(��"pack://application:,,,/Img/add.png��6$   $��20��6P   $��20��6\   $
��Left=�6g   $��1q�6�   5N       5O     -   5        $��	btExcluir��5O  m   $��28��6(   $��32��64   $
��Left=�6?   $��	34,70,0,0q�6Z   $	��Top=�6~   ��5P     �� 5Q     $2��,pack://application:,,,/Img/cancelarItens.png��6$   $��20��6Z   $��20��6f   $
��Left=�6q   $��1q�6�   5S     5V     5W     5X     �� 5Y  
   $��Bottom=�6   $��1��6,   
�������$	��810��6Q   $��60��6]   ._�5Z     � .+�5[       -   5        $��	btnSalvar��5[  $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�     BotaoMenor2#��[ 6�     5\     -   5        $��btnCancelar��5\  $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[ 6�   5]     5^     5_     5`     