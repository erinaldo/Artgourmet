   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa7    1Artebit.Restaurante.Caixa.Cadastro.FormFidelidade   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5
   	   $	��453��5   	   $	��457��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	      CanMove . $��Fidelidade  ��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5	   "   .��5      � 
������� � 	Resources 5   
      Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType	 ��Name
 � ColumnDefinitions��%�  &	        �� 5      "  6(   .�5      ��  � ��$ 25��6+   �� 5       � �� 5      �� 5      "  6*   .R�5      �� $	 borda1��6%   ���#B68944���#49181A$��0 0 0 0q�5   %   ._�5   "   � 
 5   &   �� 5   *   $��50 �6;   �� 5   *   $	��290 �6;   �� 5   *   $	��110 �6;   .+�5    &   �� ���#49181A$��1q�5"   -   $��0��5#   -   ._�5%   *   �� $��0 0 0 0q�60   
�������$��Center=�5'   4   $��11(�5(   4   $��Código��5)   4   5*   '   �� 5,   &   ���#49181A$��1q�5.   -   $��1��5/   -   ._�51   *   �� $��Center=�60   
�������$��Center=�53   4   $��11(�54   4   $��Produto��55   4   56   '   �� 59   &   ���#49181A$��1q�5;   -   $��2��5<   -   ._�5>   *   �� $��Center=�60   
�������$��Center=�5@   4   $��11(�5A   4   $��Valor por Real��5B   4   5C   '   5D   #   5E      5H      5J      5K      5L      .+�5M   
   ,  &Telerik.Windows.Controls.RadTabControl  -   5        $��radTabControl1��5M   P   $��Stretch=�6    $��0,0,0,50q�6>   $��Stretch=�6f   ;  5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�6�   .@�5O      )  #Telerik.Windows.Controls.RadTabItem  -   5        $��RadTabEditar��5O   ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�6q   $	��120��6�   ���#EEEEEE.��5P      � .+�5Q      �� -   5        $��label1��5Q   f   $��Nome:��6   $��28��6,   $
��Left=�68   $��	23,21,0,0q�6S   $	��Top=�6t   �� 5R      -   5        $��txtNome��5R   X   $��23��6   $
��Left=�6*   $��	90,21,0,0q�6E   $	��Top=�6g   $	��254��6   �� 5T      -   5        $��lbCPF��5T   k   $��Descrição:��6   $��28��61   $
��Left=�6=   $��	23,55,0,0q�6X   $	��Top=�6x   �� 5U      -   5        $��txtMoeda��5U   X   $��23��6   $
��Left=�6*   $��	90,55,0,0q�6E   $	��Top=�6h   $	��254��6�   �� 5W      -   5        $��ckbAtivo��5W   j   $��Ativo��6   $��16��6/   $
��Left=�6;   $��
380,24,0,0q�6V   $	��Top=�6z   $��77��6�   �� 5Y      -   5        $��	groupBox1��5Y   h   $
��Tipo��6   $	��102��6-   $
��Left=�6:   $��	17,98,0,0q�6U   $	��Top=�6y   $	��420��6�   .��5Z      � .+�5[      	� -	   5        $��radioProduto��5[   *   $��14, 11, 0,0 q�6>   $��Produto��6T    	� 	GroupName$
 tipo��6f   6w   	� 5\      -
   5        $��radioConsumo��5\   *   $��114, 11, 0,0 q�6>   $��Consumo��6U   $
 tipo��6g   6x   �� 5^      -   5        $��label3��5^   x   $��Pontos por Real:��6$   $��28��6?   $
��Left=�6K   $��9,41,0,0q�6f   $	��Top=�6�   /	 
 )Telerik.Windows.Controls.RadMaskedTextBox	  5_      -   5        $��	txtPontos��5_   �    	  MaskType$ Numeric=�67    	  Mask$ f3��6J   $��23��6T   $
��Left=�6`   $��
115,41,0,0q�6{   $	��Top=�6�   $	��100��6�   5b      5c      �� 5d      -   5        $��	groupBox2��5d   l   $��Horário��6   $	��110��60   $
��Left=�6=   $��
17,206,0,0q�6X   $	��Top=�6}   $	��420��6�   .��5e      � .+�5f      �� -   5        $��
ckbDiaTodo��5f   t   $��Dia Todo��6'   $��16��6:   $��	14,11,0,0q�6F   $
��Left=�6Y   $	��Top=�6�   $��77��6�   6�   �� 5g      -   5        $��label5��5g   p   $��	Horário:��6$   $��28��67   $
��Left=�6C   $��9,41,0,0q�6^   $	��Top=�6~   	  5h      -   5        $��txtHorarioIni��5h   �   $ DateTime=�67   $ hh:mm��6K   $��23��6X   $
��Left=�6d   $��	67,41,0,0q�6   $	��Top=�6�   $��50��6�   �� 5i      -   5        $��label6��5i   k   $��à��6$   $��28��60   $
��Left=�6<   $��
130,41,0,0q�6W   $	��Top=�6y   	  5j      -   5        $��txtHorarioFim��5j   �   $ DateTime=�67   $ hh:mm��6K   $��23��6X   $
��Left=�6d   $��
160,41,0,0q�6   $	��Top=�6�   $��50��6�   5k      5l      5n      5o        5q      -   5        $��RadTabProdutos��5q   Z   $��Produtos��6!   $��30��63   $
��Left=�6?   $	��Top=�6p   $	��120��6�   ���#EEEEEE.��5r      � .+�5s      *
  $Telerik.Windows.Controls.RadGridView
  -   5         
 Name$ gridProdutos��5s   *   $	��520��6@   $	��520��6P   $	��980��6]   $��0��6i   $��2��6y    J� Opacity$	 0.9Q�6�   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   CanUserFreezeColumns .     RowIndicatorVisibility$ 	Collapsed=�5t   .      AutoGenerateColumns .     
IsReadOnly . $	��Top=�6      ShowGroupPanel .     HeaderRowStyle# [  5u   =      Columns 5v      1  +Telerik.Windows.Controls.GridViewDataColumn  5w      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��69   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 50 6n      IsGroupable .     IsFilterable .     DataMemberBinding 6I   �� *codigo+6�     5x      $ Plano��69   $	 290 6k    .  .   6H   �� *nome+6�     5y      $
 Tipo��69   $	 110 6k    .   .   6G   �� *valor+6�   5{      5|      5}      5~      �� 5�   
   $��Bottom=�6   $��1��6,   $
��Left=�69   
�������$	��444��6l   $��50��6x   ._�5�      � .+�5�      (  "Telerik.Windows.Controls.RadButton  -   5        $��	btnSalvar��5�   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�     BotaoMenor2#��[ 6�     5�      -   5        $��btnCancelar��5�   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[ 6�   5�      5�      5�      5�      