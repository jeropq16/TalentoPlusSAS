using OpenAI.Chat;

namespace TalentoPlus.Web.Services
{
    public class AIService
    {
        private readonly ChatClient _client;

        public AIService(IConfiguration config)
        {
            string key = config["OpenAI:ApiKey"]!;
            _client = new ChatClient("gpt-3.5-turbo", key);
        }

        public async Task<string> GenerateProfileAsync(
            string jobTitle,
            string educationLevel,
            string department)
        {
            try
            {
                string prompt = $@"
Genera un perfil profesional breve, claro y bien redactado.
Cargo: {jobTitle}
Nivel educativo: {educationLevel}
Departamento: {department}

Escribe en tercera persona, tono profesional, 3-4 líneas.";

                ChatCompletion completion = await _client.CompleteChatAsync(prompt);

                return completion.Content[0].Text ?? "No se pudo generar el perfil.";
            }
            catch
            {
                return "No se pudo generar el perfil automáticamente.";
            }
        }
    }
}