   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa:    4Artebit.Restaurante.Caixa.Caixas.Janelas.FecharConta   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    $��Fechar Conta��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�$	��840��$	��580��
�������.��/  )Telerik.Windows.Controls.RadBusyIndicator  -     Name$ busyIndicator��   BusyContent$ Processando...��   IsBusy .  .��� .+��� -   $��label4��$
��CPF:��$��14(�$��28��$
��Left=�$��12,5,0,0q�$	��Top=� J� RenderTransformOrigin$ 
0.44,0.643&�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��txtCpf��$
��Left=�$��70,7,0,0q�$	��Top=�$	��250��$��20��   
MaskedText$  ��   Mask$ ###.###.###-##��   MaskType$ Standard=�	   TextAlignment$	 Right=�	� -   $��chkCPF��
 	� 	GroupName$
 Os��$	��CPF�� d� 	IsChecked$
 TrueY�$��
330,10,0,0q�	� -   $��chkCNPJ��$
 Os��$
��CNPJ��$��
370,10,0,0q�*  $Telerik.Windows.Controls.RadGridView  -   $��gridPagamentos��$��2��$
��Left=�$��	12,35,0,0q�$	��Top=�;  5Telerik.Windows.Controls.GridView.GridViewDataControl   ShowGroupPanel .  $	��390��   IsScrolling .  $	��460��   AutoGenerateColumns .     IsFilteringAllowed .     EditTriggers$ 	CellClick=�   GridLinesVisibility$ 
Horizontal=� � UseLayoutRounding .   � Language$ pt-BR�   RowIndicatorVisibility$ 	Collapsed=�   Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Forma Pagamento��-	  'Telerik.Windows.Controls.GridViewColumn 	  Width?
  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$
 300*
  	  
IsReadOnly .    DataMemberBinding �� *AFORMAPGTO.descricao+  $ Valor��$
 100*
  � Language$ pt-br� �� *valorRecebido+ �� StringFormat$ {0:c}��  -   $��gridRecebimentos��$��Right=�$��0,35,8,0q�$	��Top=� .  $	��390�� .  $	��340�� .   .  $ 	CellClick=�$ 
Horizontal=� .  $ pt-BR�$��1��$ 	Collapsed=�   $ Forma Pagamento��$
 200*
  .  �� *DescricaoFormaPgto+  $ Valor��$	 100
  �� *valorRecebido+$ {0:c}��  $ Excluir��$ 45
  .  	  CellTemplate �� .R�(  "Telerik.Windows.Controls.RadButton  $��	btExcluir��-   $��28��$��32��$
��Left=�$��0,0,0,0q�$	��Top=����� $2��,pack://application:,,,/Img/cancelarItens.png��$��20��$��20��$
��Left=�$��1q��� $��Bottom=�$��Right=����#EFEFEF$	��390��$	��120��$��2��$��0,0,8,40q�._�� .+��� -	   $��label3��$��Valor Recebido :��$��28��$
��Left=�$��0,0,0,0q�$	��Top=�$��16(�$��1���� -
   $��lbValorRecebido��$��28��$��Right=�$��teste��$��0,0,0,0q�$	��Top=�$��16(�$��Black"�$��1��$	��129���� -    ��Name$ label3_Copy��$��Valor Total :��$��28��$
��Left=�$��0,25,0,0q�$	��Top=�$��16(�$��1���� -   $ label3_Copy1��$��Diferença :��$��28��$
��Left=�$��0,50,0,0q�$	��Top=�$��16(�$��1���� -   $ label3_Copy2��$��Troco :��$��28��$
��Left=�$��0,75,0,0q�$	��Top=�$��16(�$��1���� -   $ lbValorTotal��$��28��$��Right=�$��teste��$��0,25,0,0q�$	��Top=�$��16(�$��Black"�$��1��$	��129���� -   $ lbValorDiferença��$��28��$��Right=�$��teste��$��0,50,0,0q�$	��Top=�$��16(�$��Black"�$��1��$	��129���� -   $ lbValorTroco��$��28��$��Right=�$��teste��$��0,75,0,0q�$	��Top=�$��16(�$��Black"�$��1��$	��129���� $��Bottom=����#EFEFEF$	��840��$��35��$��2��$��0,0,0,0q�._�� .+�  -   $��btnOK��$��OK��$��	0 0 110 0q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��0 0 20 0q�$��Right=�$��80��$��25��#��[    -   $��btPlanos��$��Plano de Fidelidade��$��25��$
��Left=�$��10,5,0,5q�#��[  $	��127��