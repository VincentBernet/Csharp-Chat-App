   M S B A M L   `   `   `  ����J  FChatRoomProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ChatRoomProjectChatRoomProject  5   	   $    ChatRoomProject.ChatRoomChoise   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   )localclr-namespace:ChatRoomProject  5   	     � Title$  ChatRoomChoise��5	   	   $	��450��6    $	��800��6-    � WindowStyle$
 None=�69    � 
ResizeMode$ NoResize=�6L    � Topmost . .��5
      � ��5   
   �� 5       �� 
StartPoint$ 0.1,0&�6"    �� EndPoint$ 0.9,1&�65   .��5      �  � Color$ #1D8348��6    � Offset$ 1Q�6/   � 5      $ #8E44AD��6   $ 0Q�6/   5      .+�5   
   �� -   5        $	��390��5      $	��Top=�6    �� CornerRadius$ 	0 0 180 0��66   ���#100E176�   �� 5   
   $	��100��6   $��	700,0,0,0q�6!   .+�5      �� -   5        	 ��Name$	 btnExit��5   "   $��22,20q�5       &  "MaterialDesignFloatingActionButton#��[  6$   
 � ToolTip$
 Close��5      ��5   `   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Close=�6*   
�������$��30��6J   $��30��6U   5      5      �� 5    
   $��Création ChatRoom��6   $��-18,66,454,339q�6-   
������� �� TextAlignment$ Center=�6X   $��30(�6o   $��Tahoma)�6}   �� 5!   
    �� Orientation$ 
Horizontal=�6   $��35,172,515,247q�6.   .+�5"        $ Forum=�6&   $��25��63   $��25��6>   
�������6J   �� 5#      -   5         ��Name$ NewChatRoomName��5#      $
��10 0q�60   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$+ %Nom de la nouvelle ChatRoom à créer��6>   
�������$	��250��6�   
������� �� 
CaretBrush �	#FFD94448 �� SelectionBrush �	#FFD944486�   5$      �� 5%   
   -   5        $	 Creationbutton��5%      $��Valider Création��6*   $
��Auto��6E   $��54,225,527,189q�6S   6k   �� 5(   
   $��Rejoindre une ChatRoom��6   $��361,65,75,340q�62   
�������$ Center=�6\   $��30(�6s   $��Tahoma)�6�   9� 5)   
   $��371,122,74,108q�6   ���#1D8348 � Cursor$ Cross��6B    9� 
IsExpanded .  J� RenderTransformOrigin$ 0.5,0.5&�6c   
�������$��5q�6�   ��5*      �� 5+      2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve6�   ���#100E17$��0,0,0,0q�6G   $��21��6X   
 �� Top$	 -10��6d   $
��Bold"�6u   $��	Underline{�6�   Liste des Rooms disponibles :6�   .��5-      �� -   5        $��InsertionPlace��5-      61   5.      50      51      