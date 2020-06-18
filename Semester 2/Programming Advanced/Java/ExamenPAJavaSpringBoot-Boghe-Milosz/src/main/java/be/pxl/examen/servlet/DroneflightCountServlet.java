package be.pxl.examen.servlet;

import be.pxl.examen.service.DroneService;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;

import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.io.PrintWriter;

@WebServlet("/droneflightcount")
public class DroneflightCountServlet extends HttpServlet {
    private static final Logger LOGGER = LogManager.getLogger(DroneflightCountServlet.class);
    public DroneflightCountServlet(){
        LOGGER.warn("Servlet is created.");
    }
    @Autowired
    private DroneService droneService;

    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws IOException {
        resp.setContentType("text/html");
        resp.setCharacterEncoding("UTF-8");
        long count = droneService.countDroneflights();
        try (PrintWriter out = resp.getWriter()) {
            out.println("<!DOCTYPE html>");
            out.println("<html>");
            out.println("<body>");
            out.println("<h1>Er zijn momenteel " + count + " droneflights.</h1>");
            out.println("</body>");
            out.println("</html>");
        }
    }
}
