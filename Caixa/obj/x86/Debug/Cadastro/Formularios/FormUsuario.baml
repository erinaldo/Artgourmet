   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa@    :Artebit.Restaurante.Caixa.Cadastro.Formularios.FormUsuario   5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p	 lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	         	 \
 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation 
   5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml  
   5      $	��400��6K   $	��600��6X   ) 	 #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6d      CanMove . $��Usuarios��5      ( 	 "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6&   
������ .��5      �  � RowDefinitions 5   
   �� 5      �� 5	      $��50 �6   ��5   
   �� 5       �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   , 	 &Telerik.Windows.Controls.RadTabControl  -   5        $��radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�5       ; 	 5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�6<   .@�5      ) 	 #Telerik.Windows.Controls.RadTabItem  -   5        $��RadTabEditar��5   ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�5   !   $	��120��69   ���#EEEEEE.��5      � $��0 15 0 0q�6    5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��3* �6(    � ColumnDefinitions 5      �� 5      �� 5      $��2* �6+   .+�5       �� $��	Usuário:��6    $��0��60   $��Center=�6=   $��Center=�6Z   $
��Bold"�5!       �� 5"      $��Nome:��6    $��1��6-   $��Center=�6:   $��Center=�6W   �� 5#      $��Senha:��6    $��2��6.   $��Center=�6;   $��Center=�6X   �� 5$      $��Ativo:��6    $��3��6.   $��Center=�6;   $��Center=�6X   /  )Telerik.Windows.Controls.RadMaskedTextBox  5%      -   5        $��txtboxCodUsuario��5%   /   $��1��6G   $��0��6W   $��25��6d      MaskType$
 None=�5&   /      EmptyContent$ Digite o código��6?   $	��270��6^   $
��Left=�6j     5'      -   5        $��
txtboxNome��5'   /   $��1��6A   $��1��6Q   $��25��6^   $
 None=�5(   /   $ Digite o nome��6?   $	��270��6\   $
��Left=�6h   G� 5)      -   5        $��senha��5)   "   $��1��6/   $��2��6?   $��25��6L   $	��270��6X   $
��Left=�5*   "   �� 5+      -   5        $��checkUsuario��5+   W   $��1��6   $��3��6/   $��Center=�6<   6W   5,      5-        5.      -   5        $��RadTabAddMesa��5.   X   $��Perfil��6!   $��30��61   $
��Left=�6=   $	��Top=�5/   !   $��5q�69   $	��120��6E   ���#EEEEEE.��50      � -   5        $��	GridPefil��50      $��3q�6(    51      �� 52      $��35 �6(   �� 53       55      �� 56      $	��171 �6+   �� 57      $
��319* �6+   �� 58      $��90 �6+   .+�5;      �� $��Sistema:��6    $
��Bold"�60   $��Center=�6B   $��Center=�5<       $��	2,10,0,10q�6;   �� 5=      $��Perfil:��6    $
��Bold"�6/   $��1��6A   $
��Left=�6Q   $��Center=�5>       $��	5,10,0,10q�6;   �� 5?      $��Supervisor:��6    $
��Bold"�63   $��2��6E   $
��Left=�6U   $��Center=�5@       �� 5B      $��1��6#   $��3��60   .��5C      � -	   5        $��
gridScroll��5C       5D      �� 5E   "   �� 5F   "   $��2* �63   �� 5G   "   $��40 �63   5I      5J      5K      5L      5M      �� 5N   
   $��Bottom=�6   $��1��6,   
�������$	��590��6Q   $��55��6]   ._�5O      � .+�5P      (  "Telerik.Windows.Controls.RadButton  -
   5        $��	btnSalvar��5P   $   $��Salvar��65   $��0 0 100 -10q�6F   $��Right=�6[   $��80��5Q   $   $��25��6/      BotaoMenor2#��[  6S     5R      -   5        $��btnCancelar��5R   $   $��Cancelar��67   $��
0 0 10 -10q�6J   $��Right=�5S   $   $��80��6@   $��25��6K   #��[  6q   5T      5U      5V      5W      