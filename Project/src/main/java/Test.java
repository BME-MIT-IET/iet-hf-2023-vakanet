import java.io.*;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Vector;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;
import java.util.stream.Stream;

public class Test {
    static final List<String> failed = new Vector<>();
    static String jarPath;

    static {
        var f = new File("out/Projlab.jar");
        if (f.exists()) {
            jarPath = f.getPath();
        } else {
            throw new RuntimeException("Start from the Code directory");
        }
    }

    static void run(Path test) {
        try {
            System.out.println("Started " + test.getFileName());
            ProcessBuilder pb = new ProcessBuilder();
            pb.command("java", "-jar", jarPath);
            var process = pb.start();

            BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(process.getOutputStream()));
            bw.write(Files.readString(test.resolve("input.txt")));
            bw.newLine();
            bw.flush();

            BufferedReader br = new BufferedReader(new InputStreamReader(process.getInputStream()));

            String line;
            var lines = new ArrayList<String>();
            while ((line = br.readLine()) != null) {
                lines.add(line);
            }

            Files.write(test.resolve("output.txt"), lines);

            var reference = Files.readAllLines(test.resolve("reference.txt"));

            var valid = true;
            if (reference.size() != lines.size()) {
                valid = false;
            } else {
                for (int i = 0; i < reference.size() && valid; i++) {
                    valid = lines.get(i).matches(reference.get(i));
                }

            }

            process.getOutputStream().close();
            process.getInputStream().close();

            if (valid) {
                System.out.println(test.getFileName() + " successful");
            } else {
                System.out.println(test.getFileName() + " failed");
                failed.add(test.getFileName().toString());
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void main(String[] args) throws IOException, InterruptedException {
        Stream<Path> tests = null;
        try{
            tests = Files.list(Paths.get("tests")).filter(Files::isDirectory);
            var executor = Executors.newFixedThreadPool(Runtime.getRuntime().availableProcessors());
            tests.forEach(path -> executor.submit(() -> run(path)));
            executor.shutdown();
            if (!executor.awaitTermination(60, TimeUnit.SECONDS))
                throw new RuntimeException("Tests timed out");

            System.out.println();

            if (failed.size() == 0) {
                System.out.println("All successful!");
            } else {
                System.out.println("Failed:");
                failed.forEach(System.out::println);
            }
        } finally {
            if(tests != null)
                tests.close();
        }
    }
}
