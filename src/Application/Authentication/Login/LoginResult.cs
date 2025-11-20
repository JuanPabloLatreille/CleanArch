namespace Application.Authentication.Login;

public record LoginResult(
    string Token, 
    string Email, 
    string Name, 
    string Role);