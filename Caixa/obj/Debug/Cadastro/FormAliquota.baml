   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormAliquota   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5
   	   $	��300��5   
   $	��457��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	      CanMove . $��	Aliquotas��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5	   "   .��5      � 
�������.+�5   
   � .+�5      �� -   5        $��label1��5   a   $��Posicao:��6   $��28��6'   $
��Left=�63   $��	23,21,0,0q�6N   $	��Top=�6o   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5      -   5        $��
txtPosicao��5   ~      MaskType$ Numeric=�6'      Mask$ f3��6:   $��23��6D   $
��Left=�6P   $��	90,21,0,0q�6k   $	��Top=�6�   $	��100��6�   �� 5      -   5        $��lbCPF��5   ^   $��Nome:��6   $��28��6$   $
��Left=�60   $��	23,55,0,0q�6K   $	��Top=�6k   �� 5      -   5        $��txtAliquota��5   P   $��23��6   $
��Left=�6"   $��	90,55,0,0q�6=   $	��Top=�6c   $	��254��6{   �� 5      -   5        $��ckbAtivo��5   s   $��Ativo��6   $��16��6'    d� 	IsChecked$
 TrueY�63   $
��Left=�6D   $��
380,24,0,0q�6_   $	��Top=�6�   $��77��6�   �� 5      -   5        $��lblTipo��5   h   $��Tipo Imposto:��6   $��28��6,   $
��Left=�68   $��23, 93, 0,0q�6S   $	��Top=�6w   	� 5      -   5        $��	radioICMS��5      $��114, 97, 0,0 q�6+   $
��ICMS��6B    	� 	GroupName$
 tipo��6Q   	� 5      -   5        $��radioISS��5      $��214, 97, 0,0 q�6*   $	��ISS��6A   $
 tipo��6O   �� 5      -	   5        $��label3��5   ^   $
��ALQ:��6   $��28��6#   $
��Left=�6/   $��
23,127,0,0q�6J   $	��Top=�6l     5      -
   5        $��txtALQ��5      $ Numeric=�6'   $ f3��6:   $��23��6D   $
��Left=�6P   $��
90,127,0,0q�6k   $	��Top=�6�   $	��100��6�   5       �� 5#   
   $��Bottom=�6   $��1��6,   $
��Left=�69   
�������$	��444��6l   $��50��6x   ._�5$      � .+�5%      (  "Telerik.Windows.Controls.RadButton  -   5        $��	btnSalvar��5%   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�      BotaoMenor2#��[  6�     5&      -   5        $��btnCancelar��5&   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   5'      5(      5*      5,      