   M S B A M L   `   `   `  ����  Artebit.Restaurante.Caixa?    9Artebit.Restaurante.Caixa.Cadastro.Formularios.FormPerfil   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      m iTelerik.Windows.Controls.Docking, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7j fTelerik.Windows.Controls.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.RibbonBar, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k
 gTelerik.Windows.Controls.Input, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.Charting, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2012.2.607.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Otelerik1http://schemas.telerik.com/2008/xaml/presentation	     	 
    5      Jd2http://schemas.microsoft.com/expression/blend/2008	     	 
    5      Tmc;http://schemas.openxmlformats.org/markup-compatibility/2006	     	 
    6@   $	��300��5      $	��600��6   )  #Telerik.Windows.Controls.WindowBase    
ResizeMode$  NoResize=�6      CanMove . $��Perfil��5   	   (  "Telerik.Windows.Controls.RadWindow   WindowStartupLocation$ CenterScreen=�6   
������ .��5      �  � RowDefinitions 5	   
   �� 5
      $��* �6   �� 5      $��50 �6   ��5   
   �� 5       �� ImageSource$0 *pack://application:,,,/Img/imagemFundo.png��6   .+�5   
   ,  &Telerik.Windows.Controls.RadTabControl  -   5          Name$ radTabControl1��5   I   $��Stretch=�6    $��0q�6>   $��Stretch=�5       ;  5Telerik.Windows.Controls.TabControl.RadTabControlBase   BackgroundVisibility$ 	Collapsed=�6<   .@�5      )  #Telerik.Windows.Controls.RadTabItem  -   5          Name$ RadTabEditar��5   ]   $��Informações��6!   $��30��66   $
��Left=�6B   $	��Top=�5   !   $	��120��69   ���#EEEEEE.��5      � $��	15 15 0 0q�6    5      �� 5      $	��40* �6(   �� 5      $	��40* �6(   �� 5      $	��40* �6(   �� 5      $	��40* �6(    � ColumnDefinitions 5      �� 5      $
��130* �6+   �� 5      $
��366* �6+   �� 5      $	��67* �6+   .+�5!      �� $��Nome:��6    $��0��6-   $��Center=�6;   $��16��6V   $��0,6,0,11q�6b   �� 5"      $��Sistema:��6    $��Center=�61   $��16��6L   $��0,1,0,17q�6X   $��1��6j   �� 5#      $��Expiração Senha(dias):��6    $��2��6>   $��Center=�6K   $��16��6f   $��0,1,0,14q�6r   / 
 )Telerik.Windows.Controls.RadMaskedTextBox  5%      -   5        	  Name$	 
txtboxNome��5%   /   $��1��6C   $��0��6S   $��30��6|   $��Center=�6�   
   MaskType$

 None=�5&   /      EmptyContent$ Digite o nome��6?   $	��270��6\   $
��Left=�6h   $	��0,4q�6�      Value 5%   `   �� *	descricao+5&   �   * 
 $Telerik.Windows.Controls.RadComboBox  5'      -   5          Name$ 	cbSistema��5'   *   $��1��6=   $��1��6M    �� DisplayMemberPath$ 	descricao��6Z    �� SelectedValuePath$ 
codSistema��6x   $��30��6�   $��Center=�6�    �� SelectedValue 6�   �� *
codSistema+6�     5)      -   5        $	 txtDiasExpira��5)   /   $��1��6F   $��2��6V   $��30��6~   $��Center=�6�   $

 None=�5*   /   $	��270��6?   $
��Left=�6K   $	��0,4q�6f    5)   c   �� *
valSenha+5*   f   �� 5,      -   5         ��Name$ chAtivo��5,      $��3��60   $��0��6=   $��3��6M   $��Ativo��6a   $��Center=�6�    d� 	IsChecked 6q   �� *ativo+6�   5/      50      51      �� 52   
   $��Bottom=�6   $��1��6,   
�������$	��600��6Q   $��55��6]   ._�53      � .+�54      (  "Telerik.Windows.Controls.RadButton  -   5          Name$ 	btnSalvar��54   $   $��Salvar��67   $��0 0 100 -10q�6H   $��Right=�6]   $��80��55   $   $��25��6/      BotaoMenor2#��[  6S     56      -   5        $ btnCancelar��56   $   $��Cancelar��69   $��
0 0 10 -10q�6L   $��Right=�57   $   $��80��6@   $��25��6K   #��[  6q   58      59      5:      5;      