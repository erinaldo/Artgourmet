   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa7    1Artebit.Restaurante.Caixa.Cadastro.FormFidelidade   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      $	��453��5   K   $	��457��6X   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d      CanMove . $��Fidelidade  ��5      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6*   
������ .��5      � 
������� � 	Resources 5   
      Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name	 � ColumnDefinitions��%s  &	        �� 5	      "  6(   .�5
      ��  � ��$ 25��6+   �� 5       � �� 5      �� 5      "  6*   .R�5      �� $ borda1��6%   ���#B68944���#49181A$��0 0 0 0q�6`   ._�5   "   � 	 5   &   �� 5   *   $��50 �6;   �� 5   *   $	��290 �6;   �� 5   *   $	��110 �6;   .+�5   &   �� ���#49181A$��1q�6C   $��0��6W   ._�5   *   �� $��0 0 0 0q�60   
�������$��Center=�6Y   $��11(�5   0   $��Código��6>   5   '   �� 5   &   ���#49181A$��1q�6C   $��1��6W   ._�5    *   �� $��Center=�60   
�������$��Center=�5!   0   $��11(�6K   $��Produto��6Y   5"   '   �� 5%   &   ���#49181A$��1q�6C   $��2��6W   ._�5'   *   �� $��Center=�60   
�������$��Center=�5(   0   $��11(�6K   $��Valor por Real��6Y   5)   '   5*   #   5+      5.      50      51      52      .+�53   
   ,  &Telerik.Windows.Controls.RadTabControl  -   5        $��radTabControl1��53   P   $��Stretch=�6    $��0,0,0,50q�6>   $��Stretch=�54       ;  5Telerik.Windows.Controls.TabControl.RadTabControlBase
   BackgroundVisibility$
 	Collapsed=�6<   .@�56      )  #Telerik.Windows.Controls.RadTabItem  -   5        $��RadTabEditar��56   ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�57   !   $	��120��69   ���#EEEEEE.��58      � .+�59      �� -   5        $��label1��59   f   $��Nome:��6   $��28��6,   $
��Left=�68   $��	23,21,0,0q�6S   $	��Top=�5:      �� 5;      -   5        $��txtNome��5;   X   $��23��6   $
��Left=�6*   $��	90,21,0,0q�6E   $	��Top=�5<      $	��254��66   �� 5>      -   5        $��lbCPF��5>   k   $��Descrição:��6   $��28��61   $
��Left=�6=   $��	23,55,0,0q�6X   $	��Top=�5?      �� 5@      -   5        $��txtMoeda��5@   X   $��23��6   $
��Left=�6*   $��	90,55,0,0q�6E   $	��Top=�5A      $	��254��66   �� 5C      -   5        $��ckbAtivo��5D      $��Ativo��5C      $��16��6/   $
��Left=�6;   $��
380,24,0,0q�6V   $	��Top=�5D   /   $��77��6G   �� 5F      -   5        $��	groupBox1��5F   h   $
��Tipo��6   $	��102��6-   $
��Left=�6:   $��	17,98,0,0q�6U   $	��Top=�5G      $	��420��67   .��5H      � .+�5I      	� -	   5        $��radioProduto��5I   *   $��14, 11, 0,0 q�6>   $��Produto��6T    	� 	GroupName$
 tipo��6f   5J   *   	� 5K      -
   5        $��radioConsumo��5K   *   $��114, 11, 0,0 q�6>   $��Consumo��6U   $
 tipo��6g   5L   *   �� 5N      -   5        $��label3��5O   $   $��Pontos por Real:��5N   $   $��28��6?   $
��Left=�6K   $��9,41,0,0q�6f   $	��Top=�5O   2   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5P      -   5        $��	txtPontos��5Q   f      MaskType$ Numeric=�5P   7      Mask$ f3��6J   $��23��6T   $
��Left=�5Q   7   $��
115,41,0,0q�6R   $	��Top=�6w   $	��100��6�   5T      5U      �� 5V      -   5        $��	groupBox2��5W      $��Horário��5V      $	��110��60   $
��Left=�6=   $��
17,206,0,0q�6X   $	��Top=�5W   0   $	��420��6H   .��5X      � .+�5Y      �� -   5        $��
ckbDiaTodo��5Z   '   $��Dia Todo��5Y   '   $��16��6:   $��	14,11,0,0q�6F   $
��Left=�6Y   $	��Top=�5Z   9   $��77��6Q   6\   �� 5[      -   5        $��label5��5\   $   $��	Horário:��5[   $   $��28��67   $
��Left=�6C   $��9,41,0,0q�6^   $	��Top=�5\   2     5]      -   5        $��txtHorarioIni��5^   e   $ DateTime=�5]   7   $ hh:mm��6K   $��23��6X   $
��Left=�5^   7   $��	67,41,0,0q�6R   $	��Top=�6z   $��50��6�   �� 5_      -   5        $��label6��5_   k   $��à��6$   $��28��60   $
��Left=�6<   $��
130,41,0,0q�6W   $	��Top=�5`   $     5a      -   5        $��txtHorarioFim��5b   f   $ DateTime=�5a   7   $ hh:mm��6K   $��23��6X   $
��Left=�5b   7   $��
160,41,0,0q�6R   $	��Top=�6{   $��50��6�   5c      5d      5f      5g        5i      -   5        $��RadTabProdutos��5i   Z   $��Produtos��6!   $��30��63   $
��Left=�6?   $	��Top=�5j   !   $	��120��69   ���#EEEEEE.��5k      � .+�5l      *  $Telerik.Windows.Controls.RadGridView  -   5          Name$ gridProdutos��5l   *   $	��520��6@   $	��520��6P   $	��980��6]   $��0��6i   $��2��5m   *    J� Opacity$	 0.9Q�67   ;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  CanUserFreezeColumns .   	  RowIndicatorVisibility$ 	Collapsed=�6b    	  AutoGenerateColumns .   	  
IsReadOnly . $	��Top=�5n   <    	  ShowGroupPanel .   	  HeaderRowStyle# [  6k    	  Columns 5o      1
  +Telerik.Windows.Controls.GridViewDataColumn
  5p      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��69   -  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 50 6n      IsGroupable .     IsFilterable .     DataMemberBinding 6I   �� *codigo+5q   M   
  5r      $ Plano��69   $	 290 6k    .  .   6H   �� *nome+5s   L   
  5t      $
 Tipo��69   $	 110 6k    .   .   6G   �� *valor+5u   M   5w      5x      5y      5z      �� 5|   
   $��Bottom=�6   $��1��6,   $
��Left=�69   
�������$	��444��6l   $��50��5}      ._�5~      � .+�5      (  "Telerik.Windows.Controls.RadButton  -   5        $��	btnSalvar��5   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��5�   $   $��25��6/     BotaoMenor2#��[ 6S     5�      -   5        $��btnCancelar��5�   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�5�   $   $��80��6@   $��25��6K   #��[ 6q   5�      5�      5�      5�      