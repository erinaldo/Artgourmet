   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormClientes   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      $	��453��5   K   $	��657��6X   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d      CanMove . $��Cliente��5      (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6%   
������ .��5      � 
�������.+�5   
   �� -   5        $��label1��5   Z   $��Nome:��6   $��28��6    $
��Left=�6,   $��	23,21,0,0q�6G   $	��Top=�5      �� 5	   
   -   5        $��txtNome��5	   L   $��23��6   $
��Left=�6   $��	74,21,0,0q�69   $	��Top=�6[   $	��274��5
      �� 5   
   -   5        $��lbCPF��5   Y   $
��CPF:��6   $��28��6   $
��Left=�6+   $��	23,55,0,0q�6F   $	��Top=�5      / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5   
   -   5        $��txtCPF��5   Q   $
��Left=�6#   $��	74,55,0,0q�6>   $	��Top=�6_   $	��152��5   #      Mask$ ###.###.###-##��6/      
MaskedText$  ��6E      MaskType$ Standard=�6S   �� 5   
   -   5        $��ckbAtivo��5   ^   $��Ativo��6   $��16��6#   $
��Left=�6/   $��
398,24,0,0q�6J   $	��Top=�5      $��77��6+   �� 5   
   -   5        $��	groupBox1��5   `   $��	Endereço��6   $	��172��6%   $
��Left=�62   $��	17,98,0,0q�6M   $	��Top=�5      $	��616��6+   .��5      � .+�5      �� -   5        $��label3��5   a   $
��Rua:��6   $��28��6'   $
��Left=�63   $��	14,11,0,0q�6N   $	��Top=�5      �� 5      -   5        $��txtRua��5   T   $��23��6   $
��Left=�6&   $��	65,11,0,0q�6A   $	��Top=�5      $	��247��62   �� 5      -	   5        $��label5��5   e   $��Número:��6   $��28��6*   $
��Left=�66   $��
324,11,0,0q�6Q   $	��Top=�5      �� 5      -
   5        $��	txtNumero��5   U   $��23��6   $
��Left=�6&   $��
412,11,0,0q�6A   $	��Top=�5      $	��120��62   �� 5       -   5        $��label4��5    d   $��Bairro:��6   $��28��6*   $
��Left=�66   $��	14,45,0,0q�6Q   $	��Top=�5!      �� 5"      -   5        $��	txtBairro��5"   T   $��23��6   $
��Left=�6&   $��	65,45,0,0q�6A   $	��Top=�5#      $	��220��62   �� 5%      -   5        $��label6��5%   j   $��Complemento:��6   $��28��6/   $
��Left=�6;   $��
324,43,0,0q�6V   $	��Top=�5&      �� 5'      -   5        $��txtComplemento��5'   U   $��23��6   $
��Left=�6&   $��
412,45,0,0q�6A   $	��Top=�5(      $	��159��62   �� 5*      -   5        $��label7��5*   d   $��Cidade:��6   $��28��6*   $
��Left=�66   $��	14,79,0,0q�6Q   $	��Top=�5+      �� 5,      -   5        $��	txtCidade��5,   T   $��23��6   $
��Left=�6&   $��	65,79,0,0q�6A   $	��Top=�5-      $	��220��62   �� 5/      -   5        $��label8��5/   a   $	��UF:��6   $��28��6&   $
��Left=�62   $��
324,79,0,0q�6M   $	��Top=�50      �� 51      -   5        $��txtUF��51   U   $��23��6   $
��Left=�6&   $��
412,79,0,0q�6A   $	��Top=�52      $	��102��62   �� 54      -   5        $��label9��54   b   $
��CEP:��6   $��28��6'   $
��Left=�63   $��
14,111,0,0q�6N   $	��Top=�55      $��38��60     56      -   5        $��txtCEP��56   \   $��Right=�6+   $��0,113,419,0q�6G   $	��Top=�57   +   $	��120��6C   $ 	#####-###��6O   $  ��6`   $ Standard=�6n   58      59      �� 5:   
   -   5        $��	groupBox2��5:   _   $��Contato��6   $��90��6$   $
��Left=�60   $��
17,276,0,0q�6K   $	��Top=�5;      $	��616��6+   .��5<      � .+�5=      �� -   5        $��label10��5=   e   $��	Telefone:��6   $��28��6,   $
��Left=�68   $��14,6,0,0q�6S   $	��Top=�5>        5?      -   5        $��txtDDD��5?   X   $
��Left=�6+   $��78,6,0,0q�6F   $	��Top=�5@   +   $��25��6C   $ ##��6N   $  ��6X   $ Standard=�6f     5A      -   5        $��txtTelefone��5A   Y   $
��Left=�6+   $��	110,6,0,0q�6F   $	��Top=�5B   +   $	��109��6C   $ ####################��6O   $  ��6k   $ Standard=�6y   �� 5D      -   5        $��label11��5D   c   $��Email:��6   $��28��6)   $
��Left=�65   $��	14,30,0,0q�6P   $	��Top=�5E      �� 5F      -   5        $��txtEmail��5F   T   $��23��6   $
��Left=�6&   $��	78,35,0,0q�6A   $	��Top=�5G      $	��195��62   5I      5J      �� 5K   
   $��Bottom=�6   $��1��6,   
�������$	��650��6Q   $��50��6]   ._�5L      � .+�5M      (  "Telerik.Windows.Controls.RadButton  -   5        $��	btnSalvar��5M   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��5N   $   $��25��6/      BotaoMenor2#��[  6S     5O      -   5        $��btnCancelar��5O   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�5P   $   $��80��6@   $��25��6K   #��[  6q   5Q      5R      5S      5T      