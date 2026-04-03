using System.Text;
using Xunit.Abstractions;

public class TestOutputTextWriter : TextWriter
{
    private readonly ITestOutputHelper _output;
    private readonly StringBuilder _buffer = new StringBuilder();

    public TestOutputTextWriter(ITestOutputHelper output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public override Encoding Encoding => Encoding.UTF8;

    public override void Write(char value)
    {
        if (value == '\n')
        {
            FlushBuffer();
        }
        else if (value != '\r')
        {
            _buffer.Append(value);
        }
    }

    public override void Write(string value)
    {
        if (string.IsNullOrEmpty(value)) return;
        var lines = value.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        for (int i = 0; i < lines.Length - 1; i++)
        {
            _buffer.Append(lines[i]);
            FlushBuffer();
        }
        _buffer.Append(lines[^1]);
    }

    public override void Flush()
    {
        FlushBuffer();
    }

    private void FlushBuffer()
    {
        if (_buffer.Length == 0) return;
        _output.WriteLine(_buffer.ToString());
        _buffer.Clear();
    }
}
