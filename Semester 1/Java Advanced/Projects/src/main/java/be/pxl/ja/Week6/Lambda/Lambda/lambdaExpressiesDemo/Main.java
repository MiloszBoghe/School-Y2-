package be.pxl.ja.Week6.Lambda.Lambda.lambdaExpressiesDemo;

public class Main {
    public static void main(String[] args) {
        DisplayOnly d =
                user -> String.format("%s [%s]", user.getName(), user.getRole());
        User user = new User("Ben", "admin");
        System.out.println(d.print(user));
    }
};

