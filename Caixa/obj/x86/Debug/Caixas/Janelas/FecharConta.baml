   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa:    4Artebit.Restaurante.Caixa.Caixas.Janelas.FecharConta   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    5   J   Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    5      $��Fechar Conta��5   V   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6l   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5      $	��840��5   [   $	��580��6g   
�������.��5      /  )Telerik.Windows.Controls.RadBusyIndicator  -   5          Name$ busyIndicator��5         BusyContent$ Processando...��66      IsBusy .  .��5      � .+�5	   
   �� -   5        $��label4��5	   f   $
��CPF:��6   $��14(�6   $��28��6-   $
��Left=�69   $��12,5,0,0q�6T   $	��Top=�5
       J� RenderTransformOrigin$ 
0.44,0.643&�6(   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5   
   -   5        $��txtCpf��5   P   $
��Left=�6#   $��70,7,0,0q�6>   $	��Top=�6^   $	��250��5   #   $��20��6/      
MaskedText$  ��6;      Mask$ ###.###.###-##��6I      MaskType$ Standard=�6_   	   TextAlignment$	 Right=�6s   	� 5   
   -   5        $��chkCPF��5   3   
 	� 	GroupName$
 Os��6   $	��CPF��6%    d� 	IsChecked$
 TrueY�6A   $��
330,10,0,0q�6R   5      	� 5   
   -   5        $��chkCNPJ��5   4   $
 Os��6   $
��CNPJ��6%   $��
370,10,0,0q�6C   6W   *  $Telerik.Windows.Controls.RadGridView  5   
   -   5        $��gridPagamentos��5   `   $��2��6   $
��Left=�62   $��	12,35,0,0q�6M   $	��Top=�5      ;  5Telerik.Windows.Controls.GridView.GridViewDataControl   ShowGroupPanel .  $	��390��6M      IsScrolling .  $	��460��6m      AutoGenerateColumns .     IsFilteringAllowed .     EditTriggers$ 	CellClick=�5   9      GridLinesVisibility$ 
Horizontal=�6R    � UseLayoutRounding .   � Language$ pt-BR�5   z      RowIndicatorVisibility$ 	Collapsed=�5   Y      Columns 5      1  +Telerik.Windows.Controls.GridViewDataColumn  5      6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Forma Pagamento��6-   -	  'Telerik.Windows.Controls.GridViewColumn 	  Width?
  9Telerik.Windows.Controls.GridView.GridViewLengthConverter$
 300*
 6F    	  
IsReadOnly .    DataMemberBinding 5   -   �� *AFORMAPGTO.descricao+6`     5      $ Valor��6-   $
 100*
 6<    � Language$ pt-br�5   n    6-   �� *valorRecebido+ �� StringFormat$ {0:c}��5      5        5    
   -   5        $��gridRecebimentos��5    L   $��Right=�6   $��0,35,8,0q�6:   $	��Top=�5!       .  $	��390��6M    .  $	��340��6m    .   .  $ 	CellClick=�5"   9   $ 
Horizontal=�6R    .  $ pt-BR�6�   $��1��5#   M   $ 	Collapsed=�6y    5$        5%      $ Forma Pagamento��6-   $
 200*
 6F    .  5&   -   �� *DescricaoFormaPgto+6^     5'      $ Valor��6-   $	 100
 6<    5(   -   �� *valorRecebido+$ {0:c}��6p     5)      $ Excluir��6-   $ 45
 6>    .  	  CellTemplate 5*      �� 5+      .R�5,      (  "Telerik.Windows.Controls.RadButton  $��	btExcluir��5-   0   -   5        $��28��5,   0   $��32��6<   $
��Left=�6G   $��0,0,0,0q�6b   $	��Top=�5-   A   ��5.   "   �� 5/   &   $2��,pack://application:,,,/Img/cancelarItens.png��6,   $��20��6b   $��20��6n   $
��Left=�50   ,   $��1q�6G   52      53      55      57      �� 5:   
   $��Bottom=�6   $��Right=�6,   ���#EFEFEF$	��390��6]   $	��120��6i   $��2��5;      $��0,0,8,40q�6%   ._�5<      � .+�5=      �� -	   5        $��label3��5>      $��Valor Recebido :��5=      $��28��63   $
��Left=�6?   $��0,0,0,0q�6Z   $	��Top=�5>   &   $��16(�6>   $��1��6L   �� 5?      -
   5        $��lbValorRecebido��5@      $��28��5?      $��Right=�6$   $��teste��6@   $��0,0,0,0q�6P   $	��Top=�5@   /   $��16(�6G   $��Black"�6U   $��1��6h   $	��129��6x   �� 5A      -   5         ��Name$ label3_Copy��5B      $��Valor Total :��5A      $��28��60   $
��Left=�6<   $��0,25,0,0q�6W   $	��Top=�5B   -   $��16(�6E   $��1��6S   �� 5C      -   5        $ label3_Copy1��5D      $��Diferença :��5C      $��28��6.   $
��Left=�6:   $��0,50,0,0q�6U   $	��Top=�5D   .   $��16(�6F   $��1��6U   �� 5E      -   5        $ label3_Copy2��5F      $��Troco :��5E      $��28��6*   $
��Left=�66   $��0,75,0,0q�6Q   $	��Top=�5F   .   $��16(�6F   $��1��6T   �� 5G      -   5        $ lbValorTotal��5H      $��28��5G      $��Right=�6$   $��teste��6@   $��0,25,0,0q�6P   $	��Top=�5H   .   $��16(�6F   $��Black"�6T   $��1��6g   $	��129��6w   �� 5I      -   5        $ lbValorDiferença��5J      $��28��5I      $��Right=�6$   $��teste��6@   $��0,50,0,0q�6P   $	��Top=�5J   2   $��16(�6J   $��Black"�6X   $��1��6k   $	��129��6{   �� 5K      -   5        $ lbValorTroco��5L      $��28��5K      $��Right=�6$   $��teste��6@   $��0,75,0,0q�6P   $	��Top=�5L   .   $��16(�6F   $��Black"�6T   $��1��6g   $	��129��6w   5M      5N      �� 5Q   
   $��Bottom=�6   ���#EFEFEF$	��840��6A   $��35��6M   $��2��6Y   $��0,0,0,0q�5R      ._�5S      � .+�5T        -   5        $��btnOK��5T   $   $��OK��61   $��	0 0 110 0q�6>   $��Right=�6Q   $��80��6m   $��25��5U   $      BotaoMenor2#��[  6D     5V      -   5        $��btnCancelar��5V   $   $��Cancelar��67   $��0 0 20 0q�6J   $��Right=�6\   $��80��5W   $   $��25��6/   #��[  6U     5X      -   5        $��btPlanos��5Y   6   $��Plano de Fidelidade��5X   $   $��25��6B   $
��Left=�6N   $��10,5,0,5q�5Y   $   #��[  6F   $	��127��6k   6w   5Z      5[      5]      5^      5_      