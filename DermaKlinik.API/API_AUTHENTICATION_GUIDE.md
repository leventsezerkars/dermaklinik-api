# DermaKlinik API Authentication Kılavuzu

Bu API, iki farklı authentication yöntemini destekler:

## 1. JWT Token Authentication (İlk Proje - Admin Panel)

### Kullanım
```http
Authorization: Bearer {jwt-token}
```

### Örnek İstek
```http
GET /api/blog
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### Login İşlemi
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}
```

## 2. API Key Authentication (İkinci Proje - Public Access)

### Kullanım
```http
X-API-Key: {api-key}
```

### Örnek İstek
```http
GET /api/blog
X-API-Key: DermaKlinik2024!@#$%^&*()_+SuperSecretApiKeyForPublicAccess
```

## Yapılandırma

### appsettings.json
```json
{
  "JwtSettings": {
    "SecretKey": "DermaKlinik2024!@#$%^&*()_+SuperSecretKeyForJWTTokenGeneration",
    "Issuer": "DermaKlinik.API",
    "Audience": "DermaKlinik.Client",
    "ExpirationInMinutes": 60
  },
  "ApiKeySettings": {
    "SecretKey": "DermaKlinik2024!@#$%^&*()_+SuperSecretApiKeyForPublicAccess",
    "HeaderName": "X-API-Key",
    "RequireHttps": false
  }
}
```

## Swagger UI Kullanımı

Swagger UI'da her iki authentication yöntemi de desteklenir:

1. **Bearer Token**: JWT token için
2. **ApiKey**: API Key için

## Güvenlik Notları

1. **JWT Token**: Kullanıcı girişi gerektirir, belirli bir süre sonra expire olur
2. **API Key**: Sürekli erişim sağlar, güvenli bir şekilde saklanmalıdır
3. **HTTPS**: Production ortamında mutlaka HTTPS kullanın
4. **API Key Rotation**: Güvenlik için API Key'leri düzenli olarak değiştirin

## Örnek Kullanım Senaryoları

### Senaryo 1: Admin Panel (JWT)
```javascript
// Login
const loginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ username: 'admin', password: 'password' })
});

const { token } = await loginResponse.json();

// API Kullanımı
const blogs = await fetch('/api/blog', {
  headers: { 'Authorization': `Bearer ${token}` }
});
```

### Senaryo 2: Public Website (API Key)
```javascript
// Sürekli erişim
const blogs = await fetch('/api/blog', {
  headers: { 'X-API-Key': 'DermaKlinik2024!@#$%^&*()_+SuperSecretApiKeyForPublicAccess' }
});
```

## Hata Kodları

- **401 Unauthorized**: Geçersiz token veya API key
- **403 Forbidden**: Yetkisiz erişim
- **400 Bad Request**: Eksik veya hatalı authentication header

## Endpoint'ler

Tüm endpoint'ler (Auth endpoint'leri hariç) her iki authentication yöntemini de destekler:

- `/api/blog` - Blog yazıları
- `/api/menu` - Menü yönetimi
- `/api/companyinfo` - Şirket bilgileri
- `/api/gallery` - Galeri yönetimi
- `/api/language` - Dil yönetimi
- `/api/user` - Kullanıcı yönetimi (sadece JWT önerilir)

## Özel Durumlar

- `/api/auth/*` endpoint'leri authentication gerektirmez
- `/api/companyinfo/active` endpoint'i `[AllowAnonymous]` attribute'una sahiptir
- `/api/user` endpoint'inin `GetAll` metodu `[AllowAnonymous]` attribute'una sahiptir
