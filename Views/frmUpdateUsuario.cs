using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AdminUsuarios.Services;
using AdminUsuarios.Infrastructure;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;
namespace AdminUsuarios.Views
{
    public partial class frmUpdateUsuario : Form
    {
        public frmUpdateUsuario()
        {
            InitializeComponent();
            InicializarTableLayoutPanel();

        }

        private void InicializarTableLayoutPanel()
        {
            tblLayoutPanel = new TableLayoutPanel();
            tblLayoutPanel.AutoSize = true;
            tblLayoutPanel.Dock = DockStyle.Top;
            tblLayoutPanel.Padding = new Padding(10);
            tblLayoutPanel.ColumnCount = 2;
            tblLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            formPanel.Controls.Add(tblLayoutPanel);  // Asegúrate de agregar el TableLayoutPanel al formPanel
        }

        private bool ValidateUserData()
        {
            // Lista de campos que pueden estar vacíos
            var allowedEmptyFields = new List<string> { "User Group", "Middle Name", "Last Name", "First Name", "Expiry Date", "Gender" };

            foreach (var key in textBoxes.Keys)
            {
                // Si el campo no está en la lista de permitidos y está vacío, mostrar mensaje de error
                if (!allowedEmptyFields.Contains(key) && string.IsNullOrWhiteSpace(textBoxes[key]?.Text))
                {
                    MessageBox.Show($"El campo '{key}' no puede estar vacío.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void ClearAndReinitializeTableLayoutPanel()
        {
            // Limpia todos los controles dentro de formPanel            
            formPanel.Controls.Clear();
            // Vuelve a crear y agregar el TableLayoutPanel
            InicializarTableLayoutPanel();
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                var userService = new UserService();
                var dataList = userService.getUrlService();
                var apiClient = new ApiClient();
                string userId = txtUserId.Text;

                UserXml user = await apiClient.GetUserAsync(userId, dataList);
                if (user != null)
                {
                    ClearAndReinitializeTableLayoutPanel();
                    PopulateUserData(user);
                }
                else
                {
                    MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateUserData(UserXml user)
        {
            var userProperties = new Dictionary<string, string>
    {
        { "Primary ID", user.PrimaryId },
        { "First Name", user.FirstName },
        { "Middle Name", user.MiddleName },
        { "Last Name", user.LastName },
        { "Gender", user.Gender },
        { "Preferred Language", user.PreferredLanguage },
        { "Status", user.Status }
    };

            foreach (var property in userProperties)
            {
                AddTextBox(property.Key, property.Value);
            }

            if (user.ContactInfo?.Addresses?.Count > 0)
            {
                var addressProperties = new Dictionary<string, string>
        {
            { "Address Line 1", user.ContactInfo.Addresses[0].Line1 },
            { "Address Line 2", user.ContactInfo.Addresses[0].Line2 },
            { "Address Line 3", user.ContactInfo.Addresses[0].Line3 },
            { "Address Line 4", user.ContactInfo.Addresses[0].Line4 },
            { "City", user.ContactInfo.Addresses[0].City },
            { "State Province", user.ContactInfo.Addresses[0].StateProvince },
            { "Postal Code", user.ContactInfo.Addresses[0].PostalCode },
            { "Country", user.ContactInfo.Addresses[0].Country }
        };

                foreach (var property in addressProperties)
                {
                    AddTextBox(property.Key, property.Value);
                }
            }

            if (user.ContactInfo?.Emails?.Count > 0)
            {
                foreach (var email in user.ContactInfo.Emails)
                {
                    // Convierte la lista de PhoneTypes a una cadena unida por comas
                    string emailTypesText = email.EmailTypes != null
                        ? string.Join(", ", email.EmailTypes)
                        : "Unknown";

                    AddTextBox($"Email Address ({emailTypesText})", email.EmailAddress);
                    
                }
            }

            if (user.ContactInfo?.Phones?.Count > 0)
            {
                foreach (var phone in user.ContactInfo.Phones)
                {
                    // Convierte la lista de PhoneTypes a una cadena unida por comas
                    string phoneTypesText = phone.PhoneTypes != null
                        ? string.Join(", ", phone.PhoneTypes)
                        : "Unknown";

                    // Agrega el texto al label con el tipo de teléfono y el número
                    AddTextBox($"Phone ({phoneTypesText})", phone.PhoneNumber);
                }
            }

            if (user.UserStatistics != null)
            {
                foreach (var stat in user.UserStatistics)
                {
                    AddTextBox($"Statistic ({stat.StatisticCategory})", stat.CategoryType);
                }
            }
        }


        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();

        private void AddTextBox(string label, string text)
        {
            // Crear un Label
            Label lbl = new Label();
            lbl.Text = label;
            lbl.AutoSize = true;
            lbl.TextAlign = ContentAlignment.MiddleLeft;

            // Crear un TextBox
            TextBox txt = new TextBox();
            txt.Text = text;
            txt.Width = 300;  // Ajustar el ancho si es necesario

            // Agregar controles al TableLayoutPanel
            tblLayoutPanel.Controls.Add(lbl);  // Agregar el Label a la columna 1
            tblLayoutPanel.Controls.Add(txt);  // Agregar el TextBox a la columna 2

            // Añadir el TextBox al diccionario para referencia posterior
            textBoxes[label] = txt;
        }


        private async void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar los datos del usuario
                //if (!ValidateUserData()) 
                //   return;
                foreach (var key in textBoxes.Keys)
                {
                    Console.WriteLine($"Key: {key}, Value: {textBoxes[key]?.Text ?? "null"}");
                }

                UserXml user = new UserXml
                {
                    PrimaryId = textBoxes.ContainsKey("Primary ID") ? textBoxes["Primary ID"]?.Text ?? string.Empty : string.Empty,
                    FirstName = textBoxes.ContainsKey("First Name") ? textBoxes["First Name"]?.Text ?? string.Empty : string.Empty,
                    LastName = textBoxes.ContainsKey("Last Name") ? textBoxes["Last Name"]?.Text ?? string.Empty : string.Empty,
                    Gender = textBoxes.ContainsKey("Gender") ? textBoxes["Gender"]?.Text ?? string.Empty : string.Empty,
                    UserGroup = textBoxes.ContainsKey("User Group") ? textBoxes["User Group"]?.Text ?? string.Empty : string.Empty,
                    PreferredLanguage = textBoxes.ContainsKey("Preferred Language") ? textBoxes["Preferred Language"]?.Text ?? string.Empty : string.Empty,
                    ExpiryDate = textBoxes.ContainsKey("Expiry Date") ? textBoxes["Expiry Date"]?.Text ?? string.Empty : string.Empty,
                    Status = textBoxes.ContainsKey("Status") ? textBoxes["Status"]?.Text ?? string.Empty : string.Empty,
                    ContactInfo = new ContactInfo
                    {
                        Addresses = new List<Address>
                {
                    new Address
                    {
                        Line1 = textBoxes.ContainsKey("Address Line 1") ? textBoxes["Address Line 1"]?.Text ?? string.Empty : string.Empty,
                        Line2 = textBoxes.ContainsKey("Address Line 2") ? textBoxes["Address Line 2"]?.Text ?? string.Empty : string.Empty,
                        Line3 = textBoxes.ContainsKey("Address Line 3") ? textBoxes["Address Line 3"]?.Text ?? string.Empty : string.Empty,
                        Line4 = textBoxes.ContainsKey("Address Line 4") ? textBoxes["Address Line 4"]?.Text ?? string.Empty : string.Empty,
                        City = textBoxes.ContainsKey("City") ? textBoxes["City"]?.Text ?? string.Empty : string.Empty,
                        StateProvince = textBoxes.ContainsKey("State Province") ? textBoxes["State Province"]?.Text ?? string.Empty : string.Empty,
                        PostalCode = textBoxes.ContainsKey("Postal Code") ? textBoxes["Postal Code"]?.Text ?? string.Empty : string.Empty,
                        Country = textBoxes.ContainsKey("Country") ? textBoxes["Country"]?.Text ?? string.Empty : string.Empty
                    }
                        },
                        Phones = new List<Phone>
                        {
                            new Phone
                            {
                                PhoneNumber = textBoxes.ContainsKey("Phone (Home)") ? textBoxes["Phone(Home)"]?.Text ?? string.Empty:string.Empty,
                                PhoneTypes = new List<string>  {"Home"}
                            },
                            new Phone
                            {
                                PhoneNumber = textBoxes.ContainsKey("Phone (Personal)") ? textBoxes["Phone(Personal)"]?.Text ?? string.Empty:string.Empty,
                                PhoneTypes = new List<string>  { "Personal" }
                            }

                        },
                        
                        Emails = new List<Email>
                        {
                            new Email
                            {
                                EmailAddress = textBoxes.ContainsKey("Email (Work)") ? textBoxes["Email (Work)"]?.Text ?? string.Empty : string.Empty,
                                EmailTypes = new List<string> { "Work" }
                            },
                            new Email
                            {
                                EmailAddress = textBoxes.ContainsKey("Email (Personal)") ? textBoxes["Email (Personal)"]?.Text ?? string.Empty : string.Empty,
                                EmailTypes = new List<string> { "Personal" }
                            }
                        }
                    }
                };

                // Generar el XML
                string userXml = GenerateUserXml(user);
                UserService userService = new UserService();
                List<CampoDTO> dataList = userService.getUrlService();
                ApiClient apiClient = new ApiClient();
                bool success = await apiClient.UpdateUserAsync(user, dataList);
                if (success)
                {
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GenerateUserXml(UserXml user)
        {
            var doc = new XDocument(
                new XElement("user",
                    new XElement("primary_id", user.PrimaryId),
                    new XElement("first_name", user.FirstName),
                    new XElement("middle_name", user.MiddleName),
                    new XElement("last_name", user.LastName),
                    new XElement("full_name", $"{user.FirstName} {user.LastName}"),
                    new XElement("gender", user.Gender),
                    new XElement("user_group", user.UserGroup),
                    new XElement("preferred_language", user.PreferredLanguage),
                    new XElement("expiry_date", user.ExpiryDate),
                    new XElement("status", user.Status),
                    new XElement("contact_info",
                        new XElement("addresses",
                            user.ContactInfo.Addresses?.Count > 0 ?
                            new XElement("address",
                                new XElement("line1", user.ContactInfo.Addresses[0].Line1),
                                new XElement("line2", user.ContactInfo.Addresses[0].Line2),
                                new XElement("line3", user.ContactInfo.Addresses[0].Line3),
                                new XElement("line4", user.ContactInfo.Addresses[0].Line4),
                                new XElement("city", user.ContactInfo.Addresses[0].City),
                                new XElement("state_province", user.ContactInfo.Addresses[0].StateProvince),
                                new XElement("postal_code", user.ContactInfo.Addresses[0].PostalCode),
                                new XElement("country", user.ContactInfo.Addresses[0].Country)
                            ) : null
                        ),
                        new XElement("emails",
                            user.ContactInfo.Emails != null ?
                            user.ContactInfo.Emails.Select(email =>
                                new XElement("email",
                                    new XElement("email_address", email.EmailAddress),
                                    email.EmailTypes != null ?
                                    new XElement("email_types",
                                        email.EmailTypes.Select(type =>
                                            new XElement("email_type", type)
                                        )
                                    ) : null
                                )
                            ) : null
                        ),
                        new XElement("phones",
                            user.ContactInfo.Phones != null ?
                            user.ContactInfo.Phones.Select(phone =>
                                new XElement("phone",
                                    new XElement("phone_number", phone.PhoneNumber),
                                    new XElement("phone_types",
                                        new XElement("phone_type", phone.PhoneTypes)
                                    )
                                )
                            ) : null
                        )
                    ),
                    new XElement("user_statistics",
                        user.UserStatistics != null ?
                        user.UserStatistics.Select(stat =>
                            new XElement("user_statistic",
                                new XElement("statistic_category", stat.StatisticCategory),
                                new XElement("category_type", stat.CategoryType)
                            )
                        ) : null
                    ),
                    new XElement("proxy_for_users")
                )
            );
            return doc.ToString();
        }
    }

}