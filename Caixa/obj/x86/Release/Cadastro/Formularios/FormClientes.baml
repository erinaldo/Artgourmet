   M S B A M L   `   `   `  ����   Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormClientes   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    $	��453��$	��657��)  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�   CanMove . $��Cliente��(  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�
������ .��� 
�������.+��� -   $��label1��$��Nome:��$��28��$
��Left=�$��	23,21,0,0q�$	��Top=��� -   $��txtNome��$��23��$
��Left=�$��	74,21,0,0q�$	��Top=�$	��274���� -   $��lbCPF��$
��CPF:��$��28��$
��Left=�$��	23,55,0,0q�$	��Top=�/ 
 )Telerik.Windows.Controls.RadMaskedTextBox  -   $��txtCPF��$
��Left=�$��	74,55,0,0q�$	��Top=�$	��152��   Mask$ ###.###.###-##��   
MaskedText$  ��   MaskType$ Standard=��� -   $��ckbAtivo��$��Ativo��$��16��$
��Left=�$��
398,24,0,0q�$	��Top=�$��77���� -   $��	groupBox1��$��	Endereço��$	��172��$
��Left=�$��	17,98,0,0q�$	��Top=�$	��616��.��� .+��� -   $��label3��$
��Rua:��$��28��$
��Left=�$��	14,11,0,0q�$	��Top=��� -   $��txtRua��$��23��$
��Left=�$��	65,11,0,0q�$	��Top=�$	��247���� -	   $��label5��$��Número:��$��28��$
��Left=�$��
324,11,0,0q�$	��Top=��� -
   $��	txtNumero��$��23��$
��Left=�$��
412,11,0,0q�$	��Top=�$	��120���� -   $��label4��$��Bairro:��$��28��$
��Left=�$��	14,45,0,0q�$	��Top=��� -   $��	txtBairro��$��23��$
��Left=�$��	65,45,0,0q�$	��Top=�$	��220���� -   $��label6��$��Complemento:��$��28��$
��Left=�$��
324,43,0,0q�$	��Top=��� -   $��txtComplemento��$��23��$
��Left=�$��
412,45,0,0q�$	��Top=�$	��159���� -   $��label7��$��Cidade:��$��28��$
��Left=�$��	14,79,0,0q�$	��Top=��� -   $��	txtCidade��$��23��$
��Left=�$��	65,79,0,0q�$	��Top=�$	��220���� -   $��label8��$	��UF:��$��28��$
��Left=�$��
324,79,0,0q�$	��Top=��� -   $��txtUF��$��23��$
��Left=�$��
412,79,0,0q�$	��Top=�$	��102���� -   $��label9��$
��CEP:��$��28��$
��Left=�$��
14,111,0,0q�$	��Top=�$��38��  -   $��txtCEP��$��Right=�$��0,113,419,0q�$	��Top=�$	��120��$ 	#####-###��$  ��$ Standard=��� -   $��	groupBox2��$��Contato��$��90��$
��Left=�$��
17,276,0,0q�$	��Top=�$	��616��.��� .+��� -   $��label10��$��	Telefone:��$��28��$
��Left=�$��14,6,0,0q�$	��Top=�  -   $��txtDDD��$
��Left=�$��78,6,0,0q�$	��Top=�$��25��$ ##��$  ��$ Standard=�  -   $��txtTelefone��$
��Left=�$��	110,6,0,0q�$	��Top=�$	��109��$ ####################��$  ��$ Standard=��� -   $��label11��$��Email:��$��28��$
��Left=�$��	14,30,0,0q�$	��Top=��� -   $��txtEmail��$��23��$
��Left=�$��	78,35,0,0q�$	��Top=�$	��195���� $��Bottom=�$��1��
�������$	��650��$��50��._�� .+�(  "Telerik.Windows.Controls.RadButton  -   $��	btnSalvar��$��Salvar��$��0 0 100 -10q�$��Right=�$��80��$��25��   BotaoMenor2#��[    -   $��btnCancelar��$��Cancelar��$��
0 0 10 -10q�$��Right=�$��80��$��25��#��[  