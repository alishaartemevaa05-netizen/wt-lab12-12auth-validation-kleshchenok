WT-12: авторизация и валидация (поверх проекта WT-8)
===================================================
Папка проекта:       wt-lab12-12auth-validation-kleshchenok
Пространство имён:   wt_lab12_12auth_validation_kleshchenok

Новые файлы:
  Models/User.cs
  Pages/Login.cshtml, Pages/Login.cshtml.cs          (вход, оформление в виде карточки)
  Pages/Register.cshtml, Pages/Register.cshtml.cs    (регистрация)
  Pages/Profile.cshtml, Pages/Profile.cshtml.cs
Заменить:
  Program.cs  (Session)
Правки вручную (_snippets):
  Data/AppDbContext.cs  — DbSet<User> и уникальный индекс Login
  Pages/Shared/_Layout.cshtml — ссылки «Регистрация», «Войти» / «Личный кабинет»
SQL тестового пользователя (по желанию): _snippets/create_user.sql
