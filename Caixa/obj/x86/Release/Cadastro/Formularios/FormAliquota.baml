   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormAliquota   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $	��300��$	��457��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��	Aliquotas��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�.��� 
�������.+�� .+��� -   $��label1��$��Posicao:��$��28��$
��Left=�$��	23,21,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��
txtPosicao��   MaskType$ Numeric=�   Mask$ f3��$��23��$
��Left=�$��	90,21,0,0q�$	��Top=�$	��100���� -   $��lbCPF��$��Nome:��$��28��$
��Left=�$��	23,55,0,0q�$	��Top=��� -   $��txtAliquota��$��23��$
��Left=�$��	90,55,0,0q�$	��Top=�$	��254���� -   $��ckbAtivo��$��Ativo��$��16�� d� 	IsChecked$
 TrueY�$
��Left=�$��
380,24,0,0q�$	��Top=�$��77���� -   $��lblTipo��$��Tipo Imposto:��$��28��$
��Left=�$��23, 93, 0,0q�$	��Top=�	� -   $��	radioICMS��$��114, 97, 0,0 q�$
��ICMS�� 	� 	GroupName$
 tipo��	� -   $��radioISS��$��214, 97, 0,0 q�$	��ISS��$
 tipo���� -	   $��label3��$
��ALQ:��$��28��$
��Left=�$��
23,127,0,0q�$	��Top=�  -
   $��txtALQ��$ Numeric=�$ f3��$��23��$
��Left=�$��
90,127,0,0q�$	��Top=�$	��100���� $��Bottom=�$��1��$
��Left=�
�������$	��444��$��50��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  