   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa7    1Artebit.Restaurante.Caixa.Cadastro.FormFidelidade   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $	��453��$	��457��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��Fidelidade  ��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������ .��� 
������� � 	Resources    Headerstyle �� 
TargetType9  3Telerik.Windows.Controls.GridView.GridViewHeaderRow �� Property �� Value �� 
TargetType ��Name	 � ColumnDefinitions��%  &	        �� "  .���  � ��$ 25����  � �� �� "  .R��� $ borda1�����#B68944���#49181A$��0 0 0 0q�._�� 	 �� $��50 ��� $	��290 ��� $	��110 �.+��� ���#49181A$��1q�$��0��._��� $��0 0 0 0q�
�������$��Center=�$��11(�$��Código���� ���#49181A$��1q�$��1��._��� $��Center=�
�������$��Center=�$��11(�$��Produto���� ���#49181A$��1q�$��2��._��� $��Center=�
�������$��Center=�$��11(�$��Valor por Real��.+�,  &Telerik.Windows.Controls.RadTabControl  -   $��radTabControl1��$��Stretch=�$��0,0,0,50q�$��Stretch=�;  5Telerik.Windows.Controls.TabControl.RadTabControlBase
   BackgroundVisibility$
 	Collapsed=�.@�)  #Telerik.Windows.Controls.RadTabItem  -   $��RadTabEditar��$��Informações��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� .+��� -   $��label1��$��Nome:��$��28��$
��Left=�$��	23,21,0,0q�$	��Top=��� -   $��txtNome��$��23��$
��Left=�$��	90,21,0,0q�$	��Top=�$	��254���� -   $��lbCPF��$��Descrição:��$��28��$
��Left=�$��	23,55,0,0q�$	��Top=��� -   $��txtMoeda��$��23��$
��Left=�$��	90,55,0,0q�$	��Top=�$	��254���� -   $��ckbAtivo��$��Ativo��$��16��$
��Left=�$��
380,24,0,0q�$	��Top=�$��77���� -   $��	groupBox1��$
��Tipo��$	��102��$
��Left=�$��	17,98,0,0q�$	��Top=�$	��420��.��� .+�	� -	   $��radioProduto��$��14, 11, 0,0 q�$��Produto�� 	� 	GroupName$
 tipo��	� -
   $��radioConsumo��$��114, 11, 0,0 q�$��Consumo��$
 tipo���� -   $��label3��$��Pontos por Real:��$��28��$
��Left=�$��9,41,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��	txtPontos��   MaskType$ Numeric=�   Mask$ f3��$��23��$
��Left=�$��
115,41,0,0q�$	��Top=�$	��100���� -   $��	groupBox2��$��Horário��$	��110��$
��Left=�$��
17,206,0,0q�$	��Top=�$	��420��.��� .+��� -   $��
ckbDiaTodo��$��Dia Todo��$��16��$��	14,11,0,0q�$
��Left=�$	��Top=�$��77���� -   $��label5��$��	Horário:��$��28��$
��Left=�$��9,41,0,0q�$	��Top=�  -   $��txtHorarioIni��$ DateTime=�$ hh:mm��$��23��$
��Left=�$��	67,41,0,0q�$	��Top=�$��50���� -   $��label6��$��à��$��28��$
��Left=�$��
130,41,0,0q�$	��Top=�  -   $��txtHorarioFim��$ DateTime=�$ hh:mm��$��23��$
��Left=�$��
160,41,0,0q�$	��Top=�$��50��  -   $��RadTabProdutos��$��Produtos��$��30��$
��Left=�$	��Top=�$	��120�����#EEEEEE.��� .+�*  $Telerik.Windows.Controls.RadGridView  -     Name$ gridProdutos��$	��520��$	��520��$	��980��$��0��$��2�� J� Opacity$	 0.9Q�;	  5Telerik.Windows.Controls.GridView.GridViewDataControl 	  CanUserFreezeColumns .   	  RowIndicatorVisibility$ 	Collapsed=� 	  AutoGenerateColumns .   	  
IsReadOnly . $	��Top=� 	  ShowGroupPanel .   	  HeaderRowStyle# [   	  Columns 1
  +Telerik.Windows.Controls.GridViewDataColumn
  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Código��-  'Telerik.Windows.Controls.GridViewColumn   Width?  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$ 50    IsGroupable .     IsFilterable .     DataMemberBinding �� *codigo+
  $ Plano��$	 290  .  .   �� *nome+
  $
 Tipo��$	 110  .   .   �� *valor+�� $��Bottom=�$��1��$
��Left=�
�������$	��444��$��50��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��  BotaoMenor2#��[   -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[ 