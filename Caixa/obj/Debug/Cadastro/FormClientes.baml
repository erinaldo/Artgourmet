   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa5    /Artebit.Restaurante.Caixa.Cadastro.FormClientes   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   m iTelerik.Windows.Controls.Docking, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2011.2.712.35, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5
   	   $	��453��5   	   $	��657��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�5   	      CanMove . $��Cliente��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�5   	   
������ &   Telerik.Windows.Controls.Theming   Theme-  'Telerik.Windows.Controls.ThemeConverter$ Transparent 5	   "   .��5      � 
�������.+�5   
   �� -   5        $��label1��5   Z   $��Nome:��6   $��28��6    $
��Left=�6,   $��	23,21,0,0q�6G   $	��Top=�6h   �� 5   
   -   5        $��txtNome��5   L   $��23��6   $
��Left=�6   $��	74,21,0,0q�69   $	��Top=�6[   $	��274��6s   �� 5   
   -   5        $��lbCPF��5   Y   $
��CPF:��6   $��28��6   $
��Left=�6+   $��	23,55,0,0q�6F   $	��Top=�6f   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5   
   -   5        $��txtCPF��5   Q   $
��Left=�6#   $��	74,55,0,0q�6>   $	��Top=�6_   $	��152��6w      Mask$ ###.###.###-##��6�      
MaskedText$  ��6�      MaskType$ Standard=�6�   �� 5   
   -   5        $��ckbAtivo��5   ^   $��Ativo��6   $��16��6#   $
��Left=�6/   $��
398,24,0,0q�6J   $	��Top=�6n   $��77��6�   �� 5   
   -   5        $��	groupBox1��5   `   $��	Endereço��6   $	��172��6%   $
��Left=�62   $��	17,98,0,0q�6M   $	��Top=�6q   $	��616��6�   .��5      � .+�5      �� -   5        $��label3��5   a   $
��Rua:��6   $��28��6'   $
��Left=�63   $��	14,11,0,0q�6N   $	��Top=�6o   �� 5      -   5        $��txtRua��5   T   $��23��6   $
��Left=�6&   $��	65,11,0,0q�6A   $	��Top=�6b   $	��247��6z   �� 5      -	   5        $��label5��5   e   $��Número:��6   $��28��6*   $
��Left=�66   $��
324,11,0,0q�6Q   $	��Top=�6s   �� 5      -
   5        $��	txtNumero��5   U   $��23��6   $
��Left=�6&   $��
412,11,0,0q�6A   $	��Top=�6f   $	��120��6~   �� 5      -   5        $��label4��5   d   $��Bairro:��6   $��28��6*   $
��Left=�66   $��	14,45,0,0q�6Q   $	��Top=�6r   �� 5      -   5        $��	txtBairro��5   T   $��23��6   $
��Left=�6&   $��	65,45,0,0q�6A   $	��Top=�6e   $	��220��6}   �� 5      -   5        $��label6��5   j   $��Complemento:��6   $��28��6/   $
��Left=�6;   $��
324,43,0,0q�6V   $	��Top=�6x   �� 5      -   5        $��txtComplemento��5   U   $��23��6   $
��Left=�6&   $��
412,45,0,0q�6A   $	��Top=�6k   $	��159��6�   �� 5!      -   5        $��label7��5!   d   $��Cidade:��6   $��28��6*   $
��Left=�66   $��	14,79,0,0q�6Q   $	��Top=�6r   �� 5"      -   5        $��	txtCidade��5"   T   $��23��6   $
��Left=�6&   $��	65,79,0,0q�6A   $	��Top=�6e   $	��220��6}   �� 5$      -   5        $��label8��5$   a   $	��UF:��6   $��28��6&   $
��Left=�62   $��
324,79,0,0q�6M   $	��Top=�6o   �� 5%      -   5        $��txtUF��5%   U   $��23��6   $
��Left=�6&   $��
412,79,0,0q�6A   $	��Top=�6b   $	��102��6z   �� 5'      -   5        $��label9��5'   b   $
��CEP:��6   $��28��6'   $
��Left=�63   $��
14,111,0,0q�6N   $	��Top=�6p   $��38��6�     5(      -   5        $��txtCEP��5(   \   $��Right=�6+   $��0,113,419,0q�6G   $	��Top=�6j   $	��120��6�   $ 	#####-###��6�   $  ��6�   $ Standard=�6�   5)      5*      �� 5+   
   -   5        $��	groupBox2��5+   _   $��Contato��6   $��90��6$   $
��Left=�60   $��
17,276,0,0q�6K   $	��Top=�6p   $	��616��6�   .��5,      � .+�5-      �� -   5        $��label10��5-   e   $��	Telefone:��6   $��28��6,   $
��Left=�68   $��14,6,0,0q�6S   $	��Top=�6t     5.      -   5        $��txtDDD��5.   X   $
��Left=�6+   $��78,6,0,0q�6F   $	��Top=�6f   $��25��6~   $ ##��6�   $  ��6�   $ Standard=�6�     5/      -   5        $��txtTelefone��5/   Y   $
��Left=�6+   $��	110,6,0,0q�6F   $	��Top=�6l   $	��109��6�   $ ####################��6�   $  ��6�   $ Standard=�6�   �� 51      -   5        $��label11��51   c   $��Email:��6   $��28��6)   $
��Left=�65   $��	14,30,0,0q�6P   $	��Top=�6r   �� 52      -   5        $��txtEmail��52   T   $��23��6   $
��Left=�6&   $��	78,35,0,0q�6A   $	��Top=�6d   $	��195��6|   54      55      �� 56   
   $��Bottom=�6   $��1��6,   
�������$	��650��6Q   $��50��6]   ._�57      � .+�58      (  "Telerik.Windows.Controls.RadButton  -   5        $��	btnSalvar��58   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��6w   $��25��6�      BotaoMenor2#��[  6�     59      -   5        $��btnCancelar��59   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�6^   $��80��6z   $��25��6�   #��[  6�   5:      5;      5<      5=      